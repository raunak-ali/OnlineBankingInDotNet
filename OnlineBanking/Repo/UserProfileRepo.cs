using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using System.Text;
using Newtonsoft.Json;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineBanking.Interfaces;
using OnlineBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;

namespace OnlineBanking.Repo
{
    public class UserProfileRepo : IUserProfile
    {
         BankingDbContext context;
         IConfiguration ?_config;

       
        public UserProfileRepo(DbContextOptions<BankingDbContext> options,IConfiguration configuration){
            context=  new BankingDbContext(options);
            _config=configuration;
            }
        public async Task<string> AddUserProfile([FromBody]UserProfile user)
        {
            
               try
 {
    if(user!=null){
        //Attach the Existing AccountUSerProfile and Account to Our new User Registration
        var existing_AccountUSer=context.AccountUserProfiles.FirstOrDefault(acc=>acc.AccountNumber==user.AccountNumber);
        var existing_Account=context.Accounts.FirstOrDefault(ac=>ac.AccountNumber==user.AccountNumber);
        user.Account=existing_Account;
        user.AccountProfile=existing_AccountUSer;

        //Set Certain Default Values(Maybe Do this Through Models Itself)


        user.IsAdmin=false;
        user.isLocked=false;
        user.extra_info="{\"tracklastattempt\":0;\"lastattemptat\":\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\"}";
//Make Logic here which sends a verification OTP to the User And also store that OTP in the extra info
        user.isConfirmedUserProfile=false;



        await context.UserProfiles.AddAsync(user);

            await context.SaveChangesAsync();

         
            return "Sucessfully Egistered the User";
    }
    return null;
            }
            catch(Exception e){
                throw;
            }
            throw new NotImplementedException();
        }


       private string generateToken(UserProfile users){
            var claims = new List<Claim>{
        new Claim("userId", users.UserId.ToString()), // Assuming userId is another field of UserProfile
        new Claim("isAdmin", users.IsAdmin.ToString()) // Include isAdmin field as a claim
    };

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        _config["Jwt:Issuer"],
        _config["Jwt:Audience"],
        claims,
        expires: DateTime.Now.AddMinutes(1),
        signingCredentials: credentials
    );
return new JwtSecurityTokenHandler().WriteToken(token);}
        public async Task<string> LoginUserProfile([FromBody]UserProfile user)
        {
            
           try{
            var UserWithUSer_id=context.UserProfiles.FirstOrDefault(s=>s.AccountNumber==user.AccountNumber);
            if(UserWithUSer_id!=null){
               string []arr=UserWithUSer_id.extra_info.Split(";");
               

               string []temp=arr[0].Split(":");
               int trackfailedattempts =int.Parse(temp[1]);
               temp=arr[1].Split(":");
               DateTime lastFailedattempt=DateTime.Parse((temp[1]+":"+temp[2]+":"+temp[3]).Replace("}","").Replace("{","").Trim('"'));
            if(trackfailedattempts>2){
                            //If More than 3 Consecutive attempts then Lock Account
                        var currobj=context.UserProfiles.FirstOrDefault(s=>s.AccountNumber==user.AccountNumber);
                        currobj.isLocked=true;
                        await context.SaveChangesAsync();
                        return"Your Account is Locked now Try Resting the password now";
                    }
            if(user!=null){
                var existing=context.UserProfiles.FirstOrDefault(s=>s.AccountNumber==user.AccountNumber && s.LoginPassword==user.LoginPassword);
                if(existing!=null){
                    //Here echeck if the User is confirmed
                    string Token=generateToken(existing);
                    //Adding that to Json response
                    var responseJson = new
                {
                    token = Token,
                    userProfile = existing
                };

                // Serialize the JSON object to a string
                string jsonResponse = JsonConvert.SerializeObject(responseJson);
                    return jsonResponse;
                    }
                else{
                   

                        if((DateTime.Now.Minute-lastFailedattempt.Minute)>60){
                            var curruserobj=context.UserProfiles.FirstOrDefault(s=>s.AccountNumber==user.AccountNumber);

                            //Making the last failedattempt at Login was less than an hour ago
                            curruserobj.extra_info="{\"tracklastattempt\":0;\"lastattemptat\":\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\"}";
                            await context.SaveChangesAsync();
                            
                        }
                        else{
                            var curruserobj=context.UserProfiles.Find(user.AccountNumber);
                             trackfailedattempts++;
                            curruserobj.extra_info="{\"tracklastattempt\":"+trackfailedattempts+";\"lastattemptat\":\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\"}";
                            await context.SaveChangesAsync();
                            return "INCORRECT PASSWORD ";
                            //If the Failed Login atttempt wa rescent then keep track
                           
                            
                        }

                
                    }
                    
                    
                    return("Cannot Log you in,Register First");
                    }
                    return null;
           }
           return null;
           }
           catch(Exception e){throw;}
        }

        public async Task<string> ResetPassword([FromBody]UserProfile user)
        {
            try{
                if(user!=null){
                    //Make Logic here which sends a verification OTP to the User(Make it so its valid only for the next 10 minutes)
                    var existing=context.UserProfiles.FirstOrDefault(s=>s.AccountNumber==user.AccountNumber);
                    if(existing.LoginPassword!=user.LoginPassword){
                    existing.LoginPassword=user.LoginPassword;
                    existing.extra_info="{\"tracklastattempt\":0;\"lastattemptat\":\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\"}";
                    await context.SaveChangesAsync();
                    return "Password submitted succesfully";}
                    else{
                        return"New password cannot be the same as existing password";
                    }
                }
                return null;
            }
            catch(Exception){
                throw;
            }
        }

//To Confirm A USer
        public async Task<string> ConfirmUserRegistration([FromBody]int userid)
        {
            try{
            var x=context.UserProfiles.FirstOrDefault(s=>s.UserId==userid);
            x.isConfirmedUserProfile=true;
            await context.SaveChangesAsync();
            
            return "USER CONFIRMED";}
            catch{throw;}

           
        }
//To Show UNConfirmed Users


      
public async Task<string> ShowUnConfirmedUsers(){
    try{
            var usersWithAccounts = await context.UserProfiles
            .Where(user => !user.isConfirmedUserProfile) // Only unconfirmed users
            .ToListAsync();

        foreach (var user in usersWithAccounts)
        {
            user.AccountProfile = await context.AccountUserProfiles.FirstOrDefaultAsync(ap => ap.AccountNumber == user.AccountNumber);
        }

        // Serialize the list of users with their associated accounts into JSON
        string jsonResponse = JsonConvert.SerializeObject(usersWithAccounts);
            return jsonResponse;
            }
            catch(Exception e){throw;}


        }


        //To Recieve the byte array of the do requested by the MVC
        public async Task<string> GetDocumentData(int accountUserId)
{
    try
    {
        // Retrieve the document data based on the accountUserId from your data source
        byte[] documentData = await context.AccountUserProfiles
            .Where(m => m.AccountUserId == accountUserId)
            .Select(m => m.ValidationDocsData)
            .FirstOrDefaultAsync();
            string base64String = Convert.ToBase64String(documentData);
    return base64String;

       
    }
    catch (Exception ex)
    {
        // Handle exception
        throw;
    }
}
//GenerateOtp and send it on the email

  public void SendEmail(string toEmail, string subject,string AccountNumber,string OTP)
        {
            try{
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            
            client.Credentials = new NetworkCredential("netasp709@gmail.com", "ndeq qwol oyew bxxr");

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("netasp709@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>User Registered</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Thank you For Registering account</p>");
            mailBody.AppendFormat("<p>Account Number: {0}</p>", AccountNumber);
            mailBody.AppendFormat("<p>Verification OTP: {0}</p>", OTP); // Include Account Number
    mailMessage.Body = mailBody.ToString();

    // Send email
    client.Send(mailMessage);}
    catch(Exception ex){
        //Lets not throw an exception here since this will not work on office VPN
        return;
    }
        }
public async Task<string> GenerateOTP(string AccountNumber){
    try{
    var existing_account=await context.AccountUserProfiles.FirstOrDefaultAsync(s=>s.AccountNumber==AccountNumber);
if(existing_account !=null){
    var toEmail=existing_account.Email_id;
     // Generate a random 6-digit number for OTP
    Random rand = new Random();
    string OTP = rand.Next(100000, 999999).ToString(); // Generates a random number between 100000 and 999999
    
    SendEmail(toEmail,"Verification Otp",AccountNumber,OTP);
    Token token=new Token();
    token.AccountNumber=AccountNumber;
    token.OTPValue=OTP;
    token.ExpiryDate=DateTime.Now.AddHours(1);
    await context.Tokens.AddAsync(token);
    await context.SaveChangesAsync();
return "Otp Sent To Your email";
}
    return "Otp Not Sent";}
    catch(Exception ex){throw;}
}
public async Task<string> CheckOTP(Token token){
    try{
        var existing_account=await context.Tokens.FirstOrDefaultAsync(
            s=>s.AccountNumber==token.AccountNumber 
        && s.OTPValue==token.OTPValue
        &&s.ExpiryDate<=token.ExpiryDate);
        context.Tokens.Remove(existing_account);
        await context.SaveChangesAsync();

        if(existing_account!=null){
            return"Otp Does Match";
            
        }
        return "OTP Does not match";
    }
    catch(Exception ex){throw;}
}



    }
}
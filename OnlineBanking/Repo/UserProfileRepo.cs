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
                    return Token;
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
                    var existing=context.UserProfiles.Find(user.AccountNumber);
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


        public async Task<string> ConfirmUserRegistration([FromBody]UserProfile user)
        {
            try{
            var x=context.UserProfiles.Find(user.UserId);
            x.isConfirmedUserProfile=true;
            await context.SaveChangesAsync();
            
            return "USER CONFIRMED";}
            catch{throw;}

           
        }


      
public async Task<IEnumerable<UserProfile>> ShowUnConfirmedUsers(){
    try{
            var ListofUsers=context.UserProfiles.Where(user=>user.isConfirmedUserProfile==false);

            return ListofUsers;}
            catch(Exception e){throw;}

        }



    }
}
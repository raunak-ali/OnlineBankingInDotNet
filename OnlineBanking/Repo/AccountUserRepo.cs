using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Interfaces;
using OnlineBanking.Models;


namespace OnlineBanking.Repo
{
    public class AccountUserRepo : IAccountUser
    {
          BankingDbContext context;
        public AccountUserRepo(DbContextOptions<BankingDbContext> options){
            context=  new BankingDbContext(options);}
       //Method used to Send an Email on Registration
         public void SendEmail(string toEmail, string subject,string AccountNumber)
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
             mailBody.AppendFormat("<p>Account Number: {0}</p>", AccountNumber); // Include Account Number
    mailMessage.Body = mailBody.ToString();

    // Send email
    client.Send(mailMessage);}
    catch(Exception ex){
        //Lets not throw an exception here since this will not work on office VPN
        return;
    }
        }
        public async Task<string> AddAccount(AccountProfile acProfile)
//Yahi validations bhi daldena
        {
             try
 {
    if(acProfile!=null){
        //Make a Default Account For the registered User
        Account acc=new Account();
        acc.Balance=0;
        acc.AccountNumber=(string)(acProfile.Mobilenumber+"0"+acProfile.AccountUserId);
        acc.Type="Savings";
        acc.AccountProfile=acProfile;
        acProfile.AccountNumber=acc.AccountNumber;
       
        await context.AddAsync(acProfile);
         await context.Accounts.AddAsync(acc);

            await context.SaveChangesAsync();

            //Once a New Account is created Lets send an Email with the Login password and generated AccountNumber
            SendEmail(acProfile.Email_id,"New Account Request is Generated",acProfile.AccountNumber);
            return "Sucessfully Inserted";
    }
    return null;
            }
            catch(Exception e){
                throw;
            }
        }

        private void SendEmail(object email_id, string v, string accountNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransaction(string AccountNumber)
        {
            try{
                var existing_account=context.UserProfiles.Where(a=>a.AccountNumber==AccountNumber).ToList();
            if(existing_account!=null){

                var List_of_transactions=context.Transactions.Where(a=>a.AccountNumber==AccountNumber).ToList();
                
                return List_of_transactions;
            }
            return null;
            }
            catch(Exception ex){
                throw;
            }
            
        }

        public async Task<string>NewPaypee(Paypee paypee){
            try{
                if(paypee!=null){
                    var existing_paypee=context.Accounts.FirstOrDefault(a=>a.AccountNumber==paypee.AccountNumber);
                    if(existing_paypee!=null){
                        await context.Paypees.AddAsync(paypee);
                        await context.SaveChangesAsync();
                        return "New Paypee Added Now";
                    }
                }
                return null;
            }
            catch(Exception ex){throw;}
        }

        public async Task<string> NewTransaction(Transaction transaction)
        {
            try{
                if (transaction !=null){
                    var existingaccount=context.Accounts.FirstOrDefault(a=>a.AccountNumber==transaction.AccountNumber);
                     var existingpaypee=context.Paypees.FirstOrDefault(p=>p.PaypeeIdAccountNumber==transaction.PaypeeIdAccountNumber);
                    if(existingaccount!=null && existingpaypee!=null){
                       
                    
                        transaction.Paypee=existingpaypee;
                        transaction.Account=existingaccount;
                        transaction.TrasactionDate=DateTime.Now;

                        await context.Transactions.AddAsync(transaction);
                        await context.SaveChangesAsync();
                        return "Transaction was Succesfull";
                        
                        }
                    

                return "Transaction was Not sucessfull";}
                return null;
            }
            catch(Exception ex){
                throw;
            }
        }
    }
}
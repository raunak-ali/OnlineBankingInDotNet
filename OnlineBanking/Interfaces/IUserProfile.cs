using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Models;

namespace OnlineBanking.Interfaces
{
    public interface IUserProfile
    {
        Task<string> AddUserProfile(UserProfile user);
        Task<string> ConfirmUserRegistration(int userid);

        Task<string> ShowUnConfirmedUsers();

        Task<string> LoginUserProfile(UserProfile user);
        Task<string> ResetPassword(UserProfile user);

        Task<string> GenerateOTP(string AccountNumber);

        Task<string> CheckOTP(Token token);

        Task<string> GetDocumentData(int accountUserId);
        //void SendEmail(string toEmail, string subject);

        
    }
}
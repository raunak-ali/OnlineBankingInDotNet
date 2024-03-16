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
        Task<string> ConfirmUserRegistration(UserProfile user);

        Task<IEnumerable<UserProfile>> ShowUnConfirmedUsers();

        Task<string> LoginUserProfile(UserProfile user);
        Task<string> ResetPassword(UserProfile user);
        //void SendEmail(string toEmail, string subject);

        
    }
}
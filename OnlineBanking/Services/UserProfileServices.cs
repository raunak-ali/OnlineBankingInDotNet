using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Interfaces;
using OnlineBanking.Models;

namespace OnlineBanking.Services
{
    public class UserProfileServices
    {
         public IUserProfile User;
        public UserProfileServices(IUserProfile User) {
    this.User = User;
}

 public async Task<string> AddAccount(UserProfile user){
     try
     {
         return await User.AddUserProfile(user);
     }
     catch(Exception ex) {
         throw;
     }
 }
 public async Task<string> LoginUserProfile(UserProfile user){

     try
     {
         return await User.LoginUserProfile(user);
     }
     catch(Exception ex) {
         throw;
     }
 }

 public async Task<string> ResetPassword(UserProfile user){

     try
     {
         return await User.ResetPassword(user);
     }
     catch(Exception ex) {
         throw;
     }
 }

  public async Task<string> ConfirmUserRegistration(UserProfile user){
    try{
        return await User.ConfirmUserRegistration(user);
    }
    catch(Exception ex){throw;}
  }


  public async Task<IEnumerable<UserProfile>> ShowUnConfirmedUsers(){
    try{
        return await User.ShowUnConfirmedUsers();
    }catch(Exception ex){throw;}
  }
    }
}
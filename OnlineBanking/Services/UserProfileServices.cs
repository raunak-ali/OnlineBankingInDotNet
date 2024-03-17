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

  public async Task<string> ConfirmUserRegistration(int userid){
    try{
        return await User.ConfirmUserRegistration(userid);
    }
    catch(Exception ex){throw;}
  }


  public async Task<string> ShowUnConfirmedUsers(){
    try{
        return await User.ShowUnConfirmedUsers();
    }catch(Exception ex){throw;}
  }
  public async Task<string> DownloadDocument(int AccountUserId){
     try{
        return await User.GetDocumentData(AccountUserId);
    }catch(Exception ex){throw;}
  }

  public async Task<string> GenerateOTP(string AccountNumber){
    try{
        return await User.GenerateOTP(AccountNumber);
    }
    catch(Exception ex){throw;}
  }
  public async Task<string> CheckOTP(Token token){
     try{
        return await User.CheckOTP(token);
    }
    catch(Exception ex){throw;}
  }
    }
}
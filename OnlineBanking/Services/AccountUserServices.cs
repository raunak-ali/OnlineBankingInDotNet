using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Interfaces;
using OnlineBanking.Models;

namespace OnlineBanking.Services
{
    public class AccountUserServices
    {
        public IAccountUser accountUser;
        public AccountUserServices(IAccountUser accountUser) {
    this.accountUser = accountUser;
}


 public async Task<string> AddAccount(AccountProfile acProfile){
     try
     {
         return await accountUser.AddAccount(acProfile);
     }
     catch(Exception ex) {
         throw;
     }
 }


 public async Task<string> AddTransaction(Transaction transaction){
     try
     {
         return await accountUser.NewTransaction(transaction);
     }
     catch(Exception ex) {
         throw;
     }
 }

 public async Task<string> NewPaypee(Paypee paypee){
     try
     {
         return await accountUser.NewPaypee(paypee);
     }
     catch(Exception ex) {
         throw;
     }
 }

 public async Task<IEnumerable<Transaction>> GetAllTransaction(string AccountNumber){
 try
     {
         return await accountUser.GetAllTransaction(AccountNumber);
     }
     catch(Exception ex) {
         throw;
     }

 }
    }
}
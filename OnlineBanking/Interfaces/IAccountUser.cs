using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Models;

namespace OnlineBanking.Interfaces
{
    public interface IAccountUser
    {
        Task<string> AddAccount(AccountProfile acProfile);
        Task<string> NewTransaction(Transaction transaction);

        Task<string> NewPaypee(Paypee paypee);

        Task<IEnumerable<Transaction>> GetAllTransaction(string AccountNumber);
        //Have User input AccountNumber
    }
}
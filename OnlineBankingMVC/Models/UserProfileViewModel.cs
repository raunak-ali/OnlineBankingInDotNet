using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Models
{
    public class UserProfileViewModel
    {
       [Key]
        public int UserId{get;set;}
        
         public bool IsAdmin { get; set; }
        public string LoginPassword{get;set;}
        public string TransactionPassword{get;set;}
        [AllowNull]
        public bool isLocked{get;set;}
         [AllowNull]

        public string extra_info{get;set;}
         [AllowNull]
   
    public bool isConfirmedUserProfile{get;set;}

    
    [AllowNull]
        public string AccountNumber{get;set;}

        [AllowNull]
        public int AccountUserId{get;set;}
   // Foreign key references
   [AllowNull]
    public AccountProfileViewModel AccountProfile { get; set; }
    [AllowNull]
    public AccountViewModel Account { get; set; }

        
        
    }
}
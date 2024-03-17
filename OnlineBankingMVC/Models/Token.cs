using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Models
{
    public class Token
    {[Key]

[AllowNull]
        public int? TokenId {get;set;}
         
        public string AccountNumber{ get; set; }

       
        [AllowNull]
        public string OTPValue{get;set;}

        [AllowNull]
        public DateTime? ExpiryDate{get;set;}
    }
}
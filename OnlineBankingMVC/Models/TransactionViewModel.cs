using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Models
{
    public class TransactionViewModel
    {
         public int TransactionId { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    [AllowNull]

    public DateTime TrasactionDate{get;set;}
    
    public string Statement { get; set; }



    // Foreign key relationship
    [AllowNull]
    public string AccountNumber { get; set; }
    [AllowNull]
    public AccountViewModel? Account { get; set; }
    [AllowNull]

    public string PaypeeIdAccountNumber{get;set;}
    [AllowNull]
    public PaypeeViewModel? Paypee { get; set; }
    }
}
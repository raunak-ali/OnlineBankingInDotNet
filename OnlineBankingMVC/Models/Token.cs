using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Models
{
    public class Token
    {
        [Key]
        public int TokenId {get;set;}
         [ForeignKey("AccountProfile")]
        public int AccountUserId{ get; set; }
        
        public string TokenValue{get;set;}
        
        public DateTime ExpiryDate{get;set;}
    }
}
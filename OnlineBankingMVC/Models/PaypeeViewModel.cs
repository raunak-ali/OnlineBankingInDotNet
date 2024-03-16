using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Models
{
    public class PaypeeViewModel
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PaypeeIdAccountNumber{get;set;}
        public string PaypeeName{get;set;}

        public string AccountNumber{get;set;}

        public string NickName{get;set;}
    }
}
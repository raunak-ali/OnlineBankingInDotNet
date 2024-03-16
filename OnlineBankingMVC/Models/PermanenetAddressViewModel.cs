using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Models
{
    public class PermanenetAddressViewModel
    {
[Key]
    public int AddressId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Landmark { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Pincode { get; set; }
    public bool IsMailingAddress { get; set; }
        [ForeignKey("AccountProfile")]
        public int AccountUserId{ get; set; }

    // Navigation property for UserProfile
    
    }
}
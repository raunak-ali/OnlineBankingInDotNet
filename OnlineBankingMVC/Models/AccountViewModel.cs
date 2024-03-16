using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using OnlineBankingMVC.Models;
namespace OnlineBankingMVC.Models
{
    public class AccountViewModel
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string AccountNumber { get; set; }
       
    public decimal Balance { get; set; }
    public string Type { get; set; }
    [AllowNull]
    public List<TransactionViewModel> Transactions { get; set; }

    // Foreign key relationship
    public int AccountUserProfileId { get; set; } // Foreign key to AccountUserProfile

    public AccountProfileViewModel AccountProfile { get; set; } // Navigation property

    // Navigation property for UserProfile

    // Navigation property for UserProfile
    }
}

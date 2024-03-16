using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using OnlineBanking.Models;
namespace OnlineBanking.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string AccountNumber { get; set; }
       
    public decimal Balance { get; set; }
    public string Type { get; set; }
    [AllowNull]
    public List<Transaction> Transactions { get; set; }

    // Foreign key relationship
    public int AccountUserProfileId { get; set; } // Foreign key to AccountUserProfile

    public AccountProfile AccountProfile { get; set; } // Navigation property

    // Navigation property for UserProfile
    }
}

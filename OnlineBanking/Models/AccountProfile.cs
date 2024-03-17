using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace OnlineBanking.Models
{
    public class AccountProfile
    {
               [Key]
               public int AccountUserId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string First_name { get; set; }

    public string Middle_name { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string Last_name { get; set; }

    public string Father_name { get; set; }

    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
    public string Mobilenumber { get; set; }

    [Required(ErrorMessage = "Email ID is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email_id { get; set; }

    [Required(ErrorMessage = "Aadhar number is required")]
    [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar number must be 12 digits")]
    public string Aadharnumber { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public string DataOfBirth { get; set; }

    // AccountNumber will be auto-generated based on phone number and primary key
    [AllowNull]
    public string AccountNumber { get; set; }

    public List<PermanenetAddress> PermanantAddress { get; set; }
    public List<ResidentialAddress> ResidentialAddress { get; set; }

    public string OccupationType { get; set; }
    public string SourceOfIncome { get; set; }

    [Required(ErrorMessage = "Gross annual income is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Gross annual income must be a positive number")]
    public string GrossAnnualIncome { get; set; }

    public bool OptedForDebitCard { get; set; }
    public bool OptedForNetBanking { get; set; }

[AllowNull]
[NotMapped]
        public IFormFile? ValidationDocs { get; set; } // File field for file uploads
  [AllowNull]
    public byte[]? ValidationDocsData { get; set; } 
  
        
    }
}

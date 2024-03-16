using Microsoft.EntityFrameworkCore;

namespace OnlineBanking.Models
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
            : base(options){}
        public DbSet<AccountProfile> AccountUserProfiles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<PermanenetAddress> PermanenetAddresses { get; set; }

                public DbSet<ResidentialAddress> ResidentialAddresses { get; set; }
                public DbSet<UserProfile> UserProfiles{get;set;}
                public DbSet<Token> Tokens{get;set;}

                public DbSet<Transaction> Transactions{get;set;}

                public DbSet<Paypee> Paypees{get;set;}


protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships
       modelBuilder.Entity<Account>()
            .HasOne(a => a.AccountProfile)
            .WithMany()
            .HasForeignKey(a => a.AccountUserProfileId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for this relationship

        modelBuilder.Entity<UserProfile>()
            .HasOne(a => a.AccountProfile)
            .WithMany()
            .HasForeignKey(a => a.AccountUserId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for this relationship
        modelBuilder.Entity<UserProfile>()
            .HasOne(a => a.Account)
            .WithMany()
            .HasForeignKey(a => a.AccountNumber)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for this relationship

        modelBuilder.Entity<Transaction>()
            .HasOne(a => a.Account)
            .WithMany()
            .HasForeignKey(a => a.AccountNumber)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for this relationship

        modelBuilder.Entity<Transaction>()
            .HasOne(a => a.Paypee)
            .WithMany()
            .HasForeignKey(a => a.PaypeeIdAccountNumber)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for this relationship
       
    }
}
}
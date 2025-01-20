using BankingWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //using options here, we will be passing con string that we have written in Appsettings.json
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
                
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server= LTIN488601\\SQLEXPRESS;Database=BankingDB;Trusted_Connection=True;TrustServerCertificate=True",
                b => b.MigrationsAssembly("BankingWebApplication"));
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Branch> Branches { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<LoanApplication> LoanApplications { get; set; }


    }
}

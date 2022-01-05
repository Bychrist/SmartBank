using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Models
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Profile>()
                .HasIndex(p => p.Email)
                .IsUnique();

            builder.Entity<Account>()
                .HasIndex(p => p.AccountNo)
                .IsUnique();


            builder.Entity<Transaction>()
               .HasIndex(p => p.TransactionRef)
               .IsUnique();


        }



        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}

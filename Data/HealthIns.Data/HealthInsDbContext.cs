using HealthIns.Data.Models;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.PrsnOrg;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data
{
   public class HealthInsDbContext : IdentityDbContext<HealthInsUser,IdentityRole,string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Premium> Premiums { get; set; }
        public DbSet<MoneyIn> MoneyIns { get; set; }
        public DbSet<ClaimActivity> ClaimActivities { get; set; }
        public HealthInsDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);
            modelBuilder.Entity<Contract>()
                .HasKey(contract => contract.Id);
            modelBuilder.Entity<Distributor>()
                 .HasKey(distributor => distributor.Id);
            modelBuilder.Entity<Organization>()
                 .HasKey(org => org.Id);
            modelBuilder.Entity<Person>()
                 .HasKey(pers => pers.Id);
            modelBuilder.Entity<Premium>()
                .HasKey(prem => prem.Id);
            modelBuilder.Entity<MoneyIn>()
               .HasKey(moneyIn => moneyIn.Id);
            modelBuilder.Entity<ClaimActivity>()
               .HasKey(claim => claim.Id);
            modelBuilder.Entity<MoneyIn>()
            .HasOne(a => a.Premium)
            .WithOne(b => b.MoneyIn)
            .HasForeignKey<Premium>(b => b.MoneyInId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

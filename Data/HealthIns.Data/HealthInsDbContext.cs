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
        public HealthInsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

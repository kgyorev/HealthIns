using HealthIns.Data.Models;
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
        public HealthInsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

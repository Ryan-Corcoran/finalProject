using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CorkyID.Models;

namespace CorkyID.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Schemes> Schemes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<SchemeUsers> SchemeUsers { get; set; }
        public DbSet<Discount> Discount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CorkyID.Models;
using CorkyID.Data;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CorkyID.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           // _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Schemes> Schemes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<SchemeUsers> SchemeUsers { get; set; }
        public DbSet<Discount> Discount { get; set; }

        public async virtual Task<List<Schemes>> GetSchemesAsync()
        {
            return await Schemes
                .AsNoTracking()
                .ToListAsync();
        }

        public async virtual Task<List<Discount>> GetDiscountsAsync()
        {
            return await Discount
                .AsNoTracking()
                .ToListAsync();
        }

        public async virtual Task AddSchemesAsync(Schemes schemes)
        {
            await Schemes.AddAsync(schemes);
            await SaveChangesAsync();
        }

        public async virtual Task AddDiscountAsync (Discount discount)
        {
            discount.LastUpdated = DateTime.UtcNow;
            await Discount.AddAsync(discount);
            await SaveChangesAsync();
        }

        public  virtual Discount GetDiscount (Guid? id)
        {
            return Discount.Find(id);
        }

        public async virtual Task DeleteSchemesAsync (Guid id)
        {
            var scheme = await Schemes.FindAsync(id);
            if (scheme != null)
            {
                Schemes.Remove(scheme);
                await SaveChangesAsync();
            }
            
        }

        public async virtual Task DeleteDiscountsAsync (Guid id)
        {
            var discount = await Discount.FindAsync(id);
            if (discount != null)
            {
                Discount.Remove(discount);
                await SaveChangesAsync();
            }
        }

        public static List<Schemes> GetSeedingSchemes()
        {
            return new List<Schemes>() {
            new Schemes() { Name = "test", SchemeDescription = "Test", ValidFromDate = new DateTime(2021, 01, 01), ValidToDate = new DateTime(2021, 12, 31) },
            new Schemes() { Name = "test2", SchemeDescription = "Test2", ValidFromDate = new DateTime(2021, 01, 01), ValidToDate = new DateTime(2021, 12, 31) },
            new Schemes() { Name = "test3", SchemeDescription = "Test3", ValidFromDate = new DateTime(2021, 01, 01), ValidToDate = new DateTime(2021, 12, 31) }
            };
        }

        public static List<Discount> GetSeedingDiscounts()
        {
            return new List<Discount>() {
                new Discount() { DiscountDescription = "test", DiscountPercentage = "1", RetailerName = "test"
                , ValidFrom = new DateTime(2021, 01, 01), ValidTo = new DateTime(2021,12,31)
                , LastUpdated = new DateTime(2021, 01, 01), LogoURL = "https://www.test.org/logotest", RedirectURL = "https://test.org"},
                new Discount() { DiscountDescription = "test2", DiscountPercentage = "1", RetailerName = "test2"
                , ValidFrom = new DateTime(2021, 01, 01), ValidTo = new DateTime(2021,12,31)
                , LastUpdated = new DateTime(2021, 01, 01), LogoURL = "https://www.test.org/logotest", RedirectURL = "https://test.org"},
                new Discount() { DiscountDescription = "test3", DiscountPercentage = "1", RetailerName = "test3"
                , ValidFrom = new DateTime(2021, 01, 01), ValidTo = new DateTime(2021,12,31)
                , LastUpdated = new DateTime(2021, 01, 01), LogoURL = "https://www.test.org/logotest", RedirectURL = "https://test.org"}
            };
        }
    }
}

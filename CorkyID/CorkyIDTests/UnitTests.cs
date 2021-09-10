using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;
using CorkyID;
using CorkyID.Data;
using CorkyID.Models;
using System.Security.Principal;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace CorkyIDTests
{
    [TestClass]
    public class DatabaseInteractionTests
    {


        [TestMethod]
        public async Task OnPostAsync_DiscountCreateAsync()
        {
            
            var identity = new GenericIdentity("testacc@testacc.com");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "12f5e1ac-a887-4551-b1bd-1ea9ad3fb9b9"));
            var principal = new GenericPrincipal(identity, new[] { "user" });

            HttpContext context = new DefaultHttpContext
            {
                User = principal
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryDB");
            var mockAppDBContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            mockAppDBContext.Setup(db => db.GetDiscountsAsync()).Returns(Task.FromResult(new List<Discount>()));
            var pageModel = new DiscountCreateModel(mockAppDBContext.Object);


            //Act
            var RedirectToPageResult = await pageModel.OnPostAsync();

            //Assert
            Assert.IsInstanceOfType(RedirectToPageResult, typeof(RedirectToPageResult));
        }

        [TestMethod]
        public async Task TestDeleteDiscounts()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions())){
                
                //Arrange
                var seedDiscounts = ApplicationDbContext.GetSeedingDiscounts();
                await db.AddRangeAsync(seedDiscounts);
                await db.SaveChangesAsync();
                var discounts = db.GetDiscountsAsync().Result;
                var discountID = discounts.First().DiscountID;
                var expectedDiscounts = seedDiscounts.Where(x => x.DiscountID != discountID).ToList();

                //Act
                await db.DeleteDiscountsAsync(discountID);

                //Assert
                var actualDiscounts = await db.Discount.AsNoTracking().ToListAsync();
                Assert.AreEqual(expectedDiscounts.OrderBy(m => m.DiscountID).Select(m => m.DiscountDescription), actualDiscounts.OrderBy(m => m.DiscountID).Select(m => m.DiscountDescription));
            }
        }

        [TestMethod]
        public async Task TestCreateDiscounts()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedDiscounts = ApplicationDbContext.GetSeedingDiscounts();
                await db.AddRangeAsync(seedDiscounts);
                await db.SaveChangesAsync();
                Discount newDisc = new Discount
                {
                    DiscountDescription = "new test description",
                    DiscountPercentage = "99",
                    ValidFrom = new DateTime(2021, 01, 01),
                    ValidTo = new DateTime(2021, 12, 31),
                    LogoURL = "test.com",
                    RedirectURL = "test.com"
                };
                var expectedCount = seedDiscounts.Count + 1;

                //Act
                await db.AddDiscountAsync(newDisc);

                //Assert
                var actualDiscounts = db.Discount.Count();
                Assert.AreEqual(expectedCount, actualDiscounts);
            }
        }

        [TestMethod]
        public async Task TestGetDiscounts()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {
                //Arrange
                var seedDiscounts = ApplicationDbContext.GetSeedingDiscounts();
                await db.AddRangeAsync(seedDiscounts);
                await db.SaveChangesAsync();
                var expectedCount = seedDiscounts.Count();
                //Act
                await db.GetDiscountsAsync();
                //Assert
                var actualCount = db.Discount.Count();
                Assert.AreEqual(expectedCount, actualCount);
            }
        }
    }
}

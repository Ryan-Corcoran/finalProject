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
        public async Task OnPostAsync_SchemeCreateAsync()
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
            mockAppDBContext.Setup(db => db.GetSchemesAsync()).Returns(Task.FromResult(new List<Schemes>()));
            var pageModel = new SchemeCreateModel(mockAppDBContext.Object);


            //Act
            var RedirectToPageResult = await pageModel.OnPostAsync();

            //Assert
            Assert.IsInstanceOfType(RedirectToPageResult, typeof(RedirectToPageResult));
        }

        [TestMethod]
        public async Task OnPostAsync_DiscountDeleteAsync()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedDiscounts = ApplicationDbContext.GetSeedingDiscounts();
                await db.AddRangeAsync(seedDiscounts);
                await db.SaveChangesAsync();
                var discount = db.Discount.First();
                var pageModel = new DiscountDeleteModel(db);
                //Act
                var redirectresult = pageModel.OnPostAsync(discount.DiscountID);

                //Assert
                Assert.IsInstanceOfType(redirectresult, typeof(Task<IActionResult>));
            }
        }

        [TestMethod]
        public async Task OnPostAsync_SchemeDeleteAsync()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedSchemes = ApplicationDbContext.GetSeedingSchemes();
                await db.AddRangeAsync(seedSchemes);
                await db.SaveChangesAsync();
                var schemes = db.Schemes.First();
                var pageModel = new SchemeDeleteModel(db);
                //Act
                var redirectresult = pageModel.OnPostAsync(schemes.SchemeID);

                //Assert
                Assert.IsInstanceOfType(redirectresult, typeof(Task<IActionResult>));
            }
        }

        [TestMethod]
        public async Task TestDeleteDiscounts()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions())){
                
                //Arrange
                var seedDiscounts = ApplicationDbContext.GetSeedingDiscounts();
                await db.AddRangeAsync(seedDiscounts);
                await db.SaveChangesAsync();
                var expectedCount = seedDiscounts.Count() - 1;
                var discounts = db.GetDiscountsAsync().Result;
                var discountID = discounts.First().DiscountID;

                //Act
                await db.DeleteDiscountsAsync(discountID);

                //Assert
                var actualDiscounts = await db.Discount.AsNoTracking().ToListAsync();
                Assert.AreEqual(expectedCount, actualDiscounts.Count());
            }
        }

        [TestMethod]
        public async Task OnGetAsync_DiscountsIndexAsync()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedDiscounts = ApplicationDbContext.GetSeedingDiscounts();
                await db.AddRangeAsync(seedDiscounts);
                await db.SaveChangesAsync();
                var schemes = db.Discount.First();
                var pageModel = new DiscountIndexModel(db);
                //Act
                var redirectresult = pageModel.OnGetAsync();

                //Assert
                Assert.IsNotNull(redirectresult);
            }
        }

        [TestMethod]
        public async Task OnGetAsync_SchemeIndexAsync()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedSchemes = ApplicationDbContext.GetSeedingSchemes();
                await db.AddRangeAsync(seedSchemes);
                await db.SaveChangesAsync();
                var pageModel = new SchemeIndexModel(db);
                //Act
                var redirectresult = pageModel.OnGetAsync();

                //Assert
                Assert.IsNotNull(redirectresult);
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

        [TestMethod]
        public async Task TestGetSchemes()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {
                //Arrange
                var SeedSchemes = ApplicationDbContext.GetSeedingSchemes();
                await db.AddRangeAsync(SeedSchemes);
                await db.SaveChangesAsync();
                var expectedCount = SeedSchemes.Count();
                //Act
                await db.GetSchemesAsync();
                //Assert
                var actualCount = db.Schemes.Count();
                Assert.AreEqual(expectedCount, actualCount);
            }
        }

        [TestMethod]
        public async Task TestDeleteSchemes()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedSchemes = ApplicationDbContext.GetSeedingSchemes();
                await db.AddRangeAsync(seedSchemes);
                await db.SaveChangesAsync();
                var expectedCount = seedSchemes.Count() - 1;
                var schemes = db.GetSchemesAsync().Result;
                var schemeID = schemes.First().SchemeID;

                //Act
                await db.DeleteSchemesAsync(schemeID);

                //Assert
                var actualSchemes = await db.Schemes.AsNoTracking().ToListAsync();
                Assert.AreEqual(expectedCount, actualSchemes.Count());
            }
        }

        [TestMethod]
        public async Task TestCreateSchemes()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                var seedSchemes = ApplicationDbContext.GetSeedingSchemes();
                await db.AddRangeAsync(seedSchemes);
                await db.SaveChangesAsync();
                Schemes newScheme = new Schemes
                {
                    Name = "test",
                    SchemeDescription = "TestDescription",
                    ValidFromDate = new DateTime(2021, 01, 01),
                    ValidToDate = new DateTime(2021,12,31)
                };
                var expectedCount = seedSchemes.Count + 1;

                //Act
                await db.AddSchemesAsync(newScheme);

                //Assert
                var actualSchemes = db.Schemes.Count();
                Assert.AreEqual(expectedCount, actualSchemes);
            }
        }
    }
}

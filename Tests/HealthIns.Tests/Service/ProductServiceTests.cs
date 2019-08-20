using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthIns.Tests.Service
{
    public class ProductServiceTests
    {
        private IProductService productService;

        private List<Product> GetDummyData()
        {
            return new List<Product>()
            {
                new Product
                {
                  Idntfr ="LIFE1",
                  Label = "Life 1",
                  FrequencyRule= "MONTHLY",
                  MaxAge=60,
                  MinAge=18

                },
                new Product
                {
                  Idntfr ="LIFE2",
                  Label = "Life 2",
                  FrequencyRule= "MONTHLY",
                  MaxAge=40,
                  MinAge=18
                }
            };
        }

        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        public ProductServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetAllProducts_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService GetAllProducts() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.productService = new ProductService(context);

            List<ProductServiceModel> actualResults = await this.productService.GetAllProducts().ToListAsync();
            List<ProductServiceModel> expectedResults = GetDummyData().To<ProductServiceModel>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];
          
                Assert.True(expectedEntry.Idntfr == actualEntry.Idntfr, errorMessagePrefix + " " + "Idntfr is not returned properly.");
                Assert.True(expectedEntry.Label == actualEntry.Label, errorMessagePrefix + " " + "Label is not returned properly.");
                Assert.True(expectedEntry.MaxAge == actualEntry.MaxAge, errorMessagePrefix + " " + "MaxAge is not returned properly.");
                Assert.True(expectedEntry.MinAge == actualEntry.MinAge, errorMessagePrefix + " " + "MinAge is not returned properly.");
                Assert.True(expectedEntry.FrequencyRule == actualEntry.FrequencyRule, errorMessagePrefix + " " + "FrequencyRule Type is not returned properly.");
            }
        }

        [Fact]
        public async Task GetAllProducts_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "ProductService GetAllProducts() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);

            List<ProductServiceModel> actualResults = await this.productService.GetAllProducts().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }


    }
}

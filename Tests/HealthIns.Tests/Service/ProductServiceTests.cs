using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Tests.Common;
using HealthIns.Web.ViewModels.Product;
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
        private IContractService contractService;

        private List<Product> GetDummyDataProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                  Id=1,
                  Idntfr ="LIFE1",
                  Label = "Life 1",
                  FrequencyRule= "MONTHLY",
                  MaxAge=60,
                  MinAge=18

                },
                new Product()
                {
                  Id=2,
                  Idntfr ="LIFE2",
                  Label = "Life 2",
                  FrequencyRule= "MONTHLY",
                  MaxAge=40,
                  MinAge=18
                }
            };
        }
        private List<Contract> GetDummyDataContract()
        {
            return new List<Contract>()
            {
                new Contract
                {
                  Id=1,
                  Frequency="MONTHLY",
                  StartDate = DateTime.Parse("01/01/2019"),
                  Product = new Product()
                {
                  Id=3,
                  Idntfr ="LIFE3",
                  Label = "Life 3",
                  FrequencyRule= "MONTHLY",
                  MaxAge=40,
                  MinAge=18
                },
                  Person=new Person()
                  {
                   StartDate=DateTime.Parse("01/01/1996")
                  }                  
                },

                new Contract
                {
                  Id=2,
                  Frequency="ANNUAL",
                  StartDate = DateTime.Parse("01/01/2019"),
                  Product = new Product()
                {
                  Id=4,
                  Idntfr ="LIFE4",
                  Label = "Life 4",
                  FrequencyRule= "MONTHLY",
                  MaxAge=40,
                  MinAge=18
                },
                  Person=new Person()
                  {
                   StartDate=DateTime.Parse("01/01/1996")
                  }
                }
            };
        }

        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyDataProduct());
            context.AddRange(GetDummyDataContract());
            await context.SaveChangesAsync();
        }

        public ProductServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        // GetAllProducts()
        [Fact]
        public async Task GetAllProducts_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService GetAllProducts() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.productService = new ProductService(context);

            List<ProductServiceModel> actualResults = await this.productService.GetAllProducts().ToListAsync();
            List<ProductServiceModel> expectedResults = GetDummyDataProduct().To<ProductServiceModel>().ToList();

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

        [Fact]
        public async Task GetById_WithExistingProduct_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            await SeedData(context);
            var actualResults = this.productService.GetById(1);
            Assert.True(actualResults !=null , errorMessagePrefix);
        }

        // ProductService GetById(long id)
        [Fact]
        public async Task GetById_WithNotExistingProduct_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            await SeedData(context);
            var actualResults = this.productService.GetById(5);
            Assert.True(actualResults == null, errorMessagePrefix);
        }


        // Task<bool> Create(ProductServiceModel productServiceModel)
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService Create(ProductServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            // await SeedData(context);

            ProductServiceModel newProduct = new ProductServiceModel()
            {   Id=1,
                Idntfr = "LIFE10",
                Label = "Life 10",
                FrequencyRule = "MONTHLY",
                MaxAge = 40,
                MinAge = 18
            };
            var actualResults = await this.productService.Create(newProduct);
            var actualEntry = this.productService.GetById(1);
            Assert.True(newProduct.Idntfr == actualEntry.Idntfr, errorMessagePrefix + " " + "Idntfr is not returned properly.");
            Assert.True(newProduct.Label == actualEntry.Label, errorMessagePrefix + " " + "Label is not returned properly.");
            Assert.True(newProduct.MaxAge == actualEntry.MaxAge, errorMessagePrefix + " " + "MaxAge is not returned properly.");
            Assert.True(newProduct.MinAge == actualEntry.MinAge, errorMessagePrefix + " " + "MinAge is not returned properly.");
            Assert.True(newProduct.FrequencyRule == actualEntry.FrequencyRule, errorMessagePrefix + " " + "FrequencyRule Type is not returned properly.");

        }

        // List<string> CheckProductRules(ContractServiceModel contract)
        [Fact]
        public async Task CheckProductRules_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService CheckProductRules(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            this.contractService = new ContractService(context);
            await SeedData(context);
            var contract= this.contractService.GetById(1);
            var actualResults =  this.productService.CheckProductRules(contract);
            Assert.True(actualResults.Count==0, errorMessagePrefix);
        }
        [Fact]
        public async Task CheckProductRules_ShouldReturnErrorResults()
        {
            string errorMessagePrefix = "ProductService CheckProductRules(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            this.contractService = new ContractService(context);
            await SeedData(context);
            var contract = this.contractService.GetById(2);
            var actualResults = this.productService.CheckProductRules(contract);
            Assert.True(actualResults.Count != 0, errorMessagePrefix);
        }

        // IQueryable<ProductServiceModel> SearchProduct(ProductSearchViewModel productSearchInputModel);
        [Fact]
        public async Task SearchProduct_ShouldReturnErrorResults()
        {
            string errorMessagePrefix = "ProductService SearchProduct(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            this.contractService = new ContractService(context);

            await SeedData(context);

            ProductSearchViewModel productSearchInputModel = new ProductSearchViewModel()
            {
                Idntfr = "LIFE2"
            };
            var actualResults = this.productService.SearchProduct(productSearchInputModel);
            Assert.True(actualResults.FirstOrDefault().Idntfr=="LIFE2", errorMessagePrefix);
        }
        // Task<bool> Update(ProductServiceModel productServiceModel);
        [Fact]
        public async Task Update_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService Update(ProductServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            await SeedData(context);
            ProductServiceModel newProduct = context.Products.First().To<ProductServiceModel>();
            newProduct.Idntfr = "LIFE10";
            newProduct.Label = "Life 10";
            newProduct.FrequencyRule = "ANNUAL";
            newProduct.MaxAge = 1;
            newProduct.MinAge = 2;

            var actualResults = await this.productService.Update(newProduct);
            var actualEntry = this.productService.GetById(newProduct.Id);
            Assert.True(newProduct.Idntfr == actualEntry.Idntfr, errorMessagePrefix + " " + "Idntfr is not returned properly.");
            Assert.True(newProduct.Label == actualEntry.Label, errorMessagePrefix + " " + "Label is not returned properly.");
            Assert.True(newProduct.MaxAge == actualEntry.MaxAge, errorMessagePrefix + " " + "MaxAge is not returned properly.");
            Assert.True(newProduct.MinAge == actualEntry.MinAge, errorMessagePrefix + " " + "MinAge is not returned properly.");
            Assert.True(newProduct.FrequencyRule == actualEntry.FrequencyRule, errorMessagePrefix + " " + "FrequencyRule Type is not returned properly.");

        }



        // ProductServiceModel GetByIdntfr(string productIdntfr);
        [Fact]
        public async Task GetByIdntfr_WithExistingProduct_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService GetByIdntfr(string productIdntfr) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            await SeedData(context);
            var actualResults = this.productService.GetByIdntfr("LIFE1");
            Assert.True(actualResults != null, errorMessagePrefix);
        }
        [Fact]
        public async Task GetByIdntfr_WithNotExistingProduct_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProductService GetByIdntfr(string productIdntfr) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.productService = new ProductService(context);
            await SeedData(context);
            var actualResults = this.productService.GetByIdntfr("LIFE133");
            Assert.True(actualResults == null, errorMessagePrefix);
        }
    }
}

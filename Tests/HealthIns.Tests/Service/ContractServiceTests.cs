using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
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
  public class ContractServiceTests
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
                  },
                  Distributor = new Distributor
                  {
                      FullName="Dist1"
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
                  },
                  Distributor = new Distributor
                  {
                      FullName="Dist1"
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

        public ContractServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }



        // ContractServiceModel GetById(long id);
        //
        // Task<bool> Create(ContractServiceModel contractServiceModel);
        // Task<bool> Update(ContractServiceModel contractServiceModel);
        // double ReturnPremiumAmount(Contract contract);
        // DateTime CalculateNextBillingDueDate(Contract contract);
        // Task<bool> TryToApplyFinancial(long contractId);
        // IQueryable<ContractServiceModel> SearchContract(ContractSearchViewModel contractSearchInputModel);
        // IQueryable<ContractServiceModel> FindContractsByDistributorId(long id);




        // IQueryable<ContractServiceModel> GetAllContracts()
        [Fact]
        public async Task GetAllContracts_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ContractService GetAllContracts() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.contractService = new ContractService(context);

            List<ContractServiceModel> actualResults = await this.contractService.GetAllContracts().ToListAsync();
            List<ContractServiceModel> expectedResults = GetDummyDataContract().To<ContractServiceModel>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Frequency == actualEntry.Frequency, errorMessagePrefix + " " + "Frequency is not returned properly.");
                Assert.True(expectedEntry.Amount == actualEntry.Amount, errorMessagePrefix + " " + "Amount is not returned properly.");
                Assert.True(expectedEntry.PremiumAmount == actualEntry.PremiumAmount, errorMessagePrefix + " " + "PremiumAmount is not returned properly.");
                Assert.True(expectedEntry.NextBillingDueDate == actualEntry.NextBillingDueDate, errorMessagePrefix + " " + "NextBillingDueDate is not returned properly.");
                Assert.True(expectedEntry.StartDate == actualEntry.StartDate, errorMessagePrefix + " " + "StartDate Type is not returned properly.");
            }
        }
        [Fact]
        public async Task GetAllContracts_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "ContractService GetAllContracts() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            List<ContractServiceModel> actualResults = await this.contractService.GetAllContracts().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }

        // ContractServiceModel GetById(long id);
        [Fact]
        public async Task GetById_WithExistingContract_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ContractService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);
            await SeedData(context);
            var actualResults = this.contractService.GetById(1);
            Assert.True(actualResults != null, errorMessagePrefix);
        }

        // ProductService GetById(long id)
        [Fact]
        public async Task GetById_WithNotExistingContract_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "ContractService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);
            await SeedData(context);
            var actualResults = this.contractService.GetById(5);
            Assert.True(actualResults == null, errorMessagePrefix);
        }



        // Task<bool> Create(ContractServiceModel contractServiceModel);
        // Task<bool> Update(ContractServiceModel contractServiceModel);
        // double ReturnPremiumAmount(Contract contract);
        // DateTime CalculateNextBillingDueDate(Contract contract);
        // Task<bool> TryToApplyFinancial(long contractId);
        // IQueryable<ContractServiceModel> SearchContract(ContractSearchViewModel contractSearchInputModel);
        // IQueryable<ContractServiceModel> FindContractsByDistributorId(long id);
    }
}

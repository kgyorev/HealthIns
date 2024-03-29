﻿using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Tests.Common;
using HealthIns.Web.ViewModels.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthIns.Tests.Service
{
  public class ContractServiceTests
    {
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
        private List<Person> GetDummyDataPerson()
        {
            return new List<Person>()
            {
                new Person()
                {
                  Id=39,
                  StartDate=DateTime.Parse("01/01/1996")

                }
            };
        }
        private List<Distributor> GetDummyDataDistributor()
        {
            return new List<Distributor>()
            {
                new Distributor()
                {
                  Id=39,
                  FullName="Dist4"

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
                  Amount=10000,
                  Status=Data.Models.Bussines.Enums.Status.InForce,
                  StartDate = DateTime.Parse("01/01/2019"),
                  Duration=10,
                  NextBillingDueDate =DateTime.Parse("01/01/2019"),
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
                   Id=390,
                   StartDate=DateTime.Parse("01/01/1996")
                  },
                  Distributor = new Distributor
                  {
                      Id=391,
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
                   Id=390,
                   StartDate=DateTime.Parse("01/01/1996")
                  },
                  Distributor = new Distributor
                  {
                      Id=49,
                      FullName="Dist2"
                  }
                },
                new Contract
                {
                  Id=3,
                  Frequency="ANNUAL",
                  StartDate = DateTime.Parse("01/01/2019"),
                  Status = Data.Models.Bussines.Enums.Status.Canceled,
                  Product = new Product()
                {
                  Id=5,
                  Idntfr ="LIFE5",
                  Label = "Life 5",
                  FrequencyRule= "MONTHLY",
                  MaxAge=40,
                  MinAge=18
                },
                  Person=new Person()
                  {
                        Id=49,
                   StartDate=DateTime.Parse("01/01/1996")
                  },
                  Distributor = new Distributor
                  {
                        Id=50,
                      FullName="Dist3"
                  }
                }
            };
        }

        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyDataProduct());
            context.AddRange(GetDummyDataPerson());
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
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ContractService Create(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);
            await SeedData(context);

            ContractServiceModel newContract = new ContractServiceModel()
            {
                Id = 4,
                Frequency = "ANNUAL",
                StartDate = DateTime.Parse("01/01/2019"),
                Duration = 10,
                ProductIdntfr = "LIFE1",
                PersonId = 39,
                DistributorId = 39
            };
            var actualResults = await this.contractService.Create(newContract);
            var actualEntry = this.contractService.GetById(4);
            Assert.True(newContract.Frequency == actualEntry.Frequency, errorMessagePrefix + " " + "Frequency is not returned properly.");
            Assert.True(newContract.StartDate == actualEntry.StartDate, errorMessagePrefix + " " + "StartDate is not returned properly.");
        }

        // Task<bool> Update(ContractServiceModel contractServiceModel);
        [Fact]
        public async Task Update_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ContractService Update(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);
            await SeedData(context);
            ContractServiceModel contract = context.Contracts.First().To<ContractServiceModel>();
            contract.NextBillingDueDate = DateTime.Now;
            contract.StartDate = DateTime.Now;
            contract.Amount = 10000;
            contract.Frequency = "ANNUAL";
            contract.Duration = 12;


            var actualResults = await this.contractService.Update(contract);
            var actualEntry = this.contractService.GetById(contract.Id);
            Assert.True(contract.NextBillingDueDate == actualEntry.NextBillingDueDate, errorMessagePrefix + " " + "NextBillingDueDate is not returned properly.");
            Assert.True(contract.StartDate == actualEntry.StartDate, errorMessagePrefix + " " + "StartDate is not returned properly.");
            Assert.True(contract.Amount == actualEntry.Amount, errorMessagePrefix + " " + "Amount is not returned properly.");
            Assert.True(contract.Frequency == actualEntry.Frequency, errorMessagePrefix + " " + "Frequency is not returned properly.");
            Assert.True(contract.Duration == actualEntry.Duration, errorMessagePrefix + " " + "Duration is not returned properly.");

        }
        // double ReturnPremiumAmount(Contract contract);
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10000,1.67)]
        [InlineData(20000, 3.33)]
        [InlineData(40000, 6.67)]
        [InlineData(50000,8.33)]
        [InlineData(100000, 16.67)]
        [InlineData(300000,50)]
        public async Task ReturnPremiumAmount_ShouldReturnCorrectResults(double benefitAmount,double expectedAmount)
        {
            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);
            await SeedData(context);
            var actualEntry = this.contractService.GetById(1);
            Contract contract = context.Contracts.SingleOrDefault(c=>c.Id==1);
            contract.Amount = benefitAmount;
            await context.SaveChangesAsync();
            var amount = this.contractService.ReturnPremiumAmount(contract);
            Assert.Equal(expectedAmount, amount);
        }
        // DateTime CalculateNextBillingDueDate(Contract contract);
        [Theory]
        [InlineData("01/01/2019","MONTHLY", "01/02/2019")]
        [InlineData("01/02/2019","ANNUAL", "01/02/2020")]
        [InlineData("01/04/2019","TRIMESTER", "01/07/2019")]
        [InlineData("01/06/2019","SEMI_ANNUAL", "01/12/2019")]
        public async Task CalculateNextBillingDueDate_ShouldReturnCorrectResults(string nextDate,string frequency,string expectedDate)
        {
            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);
            await SeedData(context);
            var actualEntry = this.contractService.GetById(1);
            Contract contract = context.Contracts.SingleOrDefault(c => c.Id == 1);
            contract.NextBillingDueDate = DateTime.Parse(nextDate);
            contract.Frequency = frequency;
            var nextBillingDueDate = this.contractService.CalculateNextBillingDueDate(contract);
            Assert.Equal(DateTime.Parse(expectedDate).ToShortDateString(), nextBillingDueDate.ToShortDateString());
        }

        // IQueryable<ContractServiceModel> SearchContract(ContractSearchViewModel contractSearchInputModel);
        [Fact]
        public async Task SearchContract_ShouldReturnResults()
        {
            string errorMessagePrefix = "ContractService SearchContract(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            ContractSearchViewModel contractSearchInputModel = new ContractSearchViewModel()
            {
                CntrctId = "1",
                Status="InForce"
            };
            var actualResults = this.contractService.SearchContract(contractSearchInputModel);
            Assert.True(actualResults.FirstOrDefault().Id == 1, errorMessagePrefix);
            Assert.True(actualResults.FirstOrDefault().Status == Data.Models.Bussines.Enums.Status.InForce, errorMessagePrefix);
        }
        [Fact]
        public async Task SearchContract_EmptyStatus_ShouldReturnResults()
        {
            string errorMessagePrefix = "ContractService SearchContract(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            ContractSearchViewModel contractSearchInputModel = new ContractSearchViewModel()
            {
                CntrctId = "1",
                Status = ""
            };
            var actualResults = this.contractService.SearchContract(contractSearchInputModel);
            Assert.True(actualResults.FirstOrDefault().Id == 1, errorMessagePrefix);
        }
        [Fact]
        public async Task SearchContract_EmptyId_ShouldReturnResults()
        {
            string errorMessagePrefix = "ContractService SearchContract(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            ContractSearchViewModel contractSearchInputModel = new ContractSearchViewModel()
            {
                CntrctId = "",
                Status = "InForce"
            };
            var actualResults = this.contractService.SearchContract(contractSearchInputModel);
            Assert.True(actualResults.Any(), errorMessagePrefix);
        }
        [Fact]
        public async Task SearchContract_EmptyId_Canceled_ShouldReturnResults()
        {
            string errorMessagePrefix = "ContractService SearchContract(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            ContractSearchViewModel contractSearchInputModel = new ContractSearchViewModel()
            {
                CntrctId = "",
                Status = "Canceled"
            };
            var actualResults = this.contractService.SearchContract(contractSearchInputModel);
            Assert.True(actualResults.Any(), errorMessagePrefix);
        }
        [Fact]
        public async Task SearchContract_EmptyCriteria_ShouldReturnAny()
        {
            string errorMessagePrefix = "ContractService SearchContract(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            ContractSearchViewModel contractSearchInputModel = new ContractSearchViewModel()
            {
            };
            var actualResults = this.contractService.SearchContract(contractSearchInputModel);
            Assert.True(actualResults.Any(), errorMessagePrefix);
        }


        // IQueryable<ContractServiceModel> FindContractsByDistributorId(long id);
        [Fact]
        public async Task FindContractsByDistributorId_ShouldReturnAny()
        {
            string errorMessagePrefix = "ContractService FindContractsByDistributorId(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            ContractSearchViewModel contractSearchInputModel = new ContractSearchViewModel()
            {
            };
            var distributor = context.Distributors.SingleOrDefault(d=>d.FullName=="Dist3");
            var actualResults = this.contractService.FindContractsByDistributorId(distributor.Id);
            Assert.True(actualResults.Any(), errorMessagePrefix);
        }


        // Task<bool> TryToApplyFinancial(long contractId);
        [Fact]
        public async Task TryToApplyFinancial_ShouldApply()
        {
            string errorMessagePrefix = "ContractService TryToApplyFinancial(long contractId) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            var actualResults = this.contractService.GetById(1);
            var contract = context.Contracts.SingleOrDefault(c => c.Id==1);

            var premium = new Premium()
            {
                Id=22,
                Contract = contract,
                Status = Data.Models.Financial.Enums.Status.Pending
            };
            var moneyIn = new MoneyIn()
            {
                Id=55,
                Contract = contract,
                Status = Data.Models.Financial.Enums.Status.Pending
            };
            context.Add(premium);
            context.Add(moneyIn);
            await context.SaveChangesAsync();
            var result = contractService.TryToApplyFinancial(1);
            Assert.True(premium.Status==Data.Models.Financial.Enums.Status.Paid, errorMessagePrefix);
            Assert.True(moneyIn.Status == Data.Models.Financial.Enums.Status.Paid, errorMessagePrefix);
        }

        // Task<bool> TryToApplyFinancial(long contractId);
        [Fact]
        public async Task TryToApplyFinancial_PaidAndPending_ShouldNotApply()
        {
            string errorMessagePrefix = "ContractService TryToApplyFinancial(long contractId) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            var actualResults = this.contractService.GetById(1);
            var contract = context.Contracts.SingleOrDefault(c => c.Id == 1);

            var premium = new Premium()
            {
                Contract = contract,
                Status = Data.Models.Financial.Enums.Status.Paid
            };
            var moneyIn = new MoneyIn()
            {
                Contract = contract,
                Status = Data.Models.Financial.Enums.Status.Pending
            };
            context.Add(premium);
            context.Add(moneyIn);
            await context.SaveChangesAsync();
            var result = contractService.TryToApplyFinancial(1);
            Assert.True(premium.Status == Data.Models.Financial.Enums.Status.Paid, errorMessagePrefix);
            Assert.True(moneyIn.Status == Data.Models.Financial.Enums.Status.Pending, errorMessagePrefix);
        }
        [Fact]
        public async Task TryToApplyFinancial_ShouldNotApply()
        {
            string errorMessagePrefix = "ContractService TryToApplyFinancial(long contractId) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.contractService = new ContractService(context);

            await SeedData(context);

            var actualResults = this.contractService.GetById(1);
            var contract = context.Contracts.SingleOrDefault(c => c.Id == 1);

            var moneyIn = new MoneyIn()
            {
                Contract = contract,
                Status = Data.Models.Financial.Enums.Status.Pending
            };
            context.Add(moneyIn);
            await context.SaveChangesAsync();
            var result = contractService.TryToApplyFinancial(1);
            Assert.True(moneyIn.Status == Data.Models.Financial.Enums.Status.Pending, errorMessagePrefix);
        }
    }
    }

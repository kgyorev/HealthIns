using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services;
using HealthIns.Services.Models;
using HealthIns.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthIns.Tests.Service
{
    public class PremiumServiceTests
    {
        private IPremiumService premiumService;
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
                  Amount=10000,
                  PremiumAmount = 200,
                  StartDate = DateTime.Parse("01/01/2019"),
                  NextBillingDueDate = DateTime.Parse("01/01/2019"),
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

        private List<Premium> GetDummyDataPremium()
        {

            return new List<Premium>()
            {
                new Premium()
                {
                    Id = 1223,
                    Contract = new Contract
                    {
                        Id = 3,
                        Frequency = "ANNUAL",
                        StartDate = DateTime.Parse("01/01/2019"),
                        Status = Data.Models.Bussines.Enums.Status.Canceled,
                        Product = new Product()
                        {
                            Id = 5,
                            Idntfr = "LIFE5",
                            Label = "Life 5",
                            FrequencyRule = "MONTHLY",
                            MaxAge = 40,
                            MinAge = 18
                        },
                        Person = new Person()
                        {
                            Id = 49,
                            StartDate = DateTime.Parse("01/01/1996")
                        },
                        Distributor = new Distributor
                        {
                            Id = 50,
                            FullName = "Dist3"
                        }
                    }

                },
                new Premium()
                {
                    Id = 1224,
                    Contract = new Contract
                    {
                        Id = 4,
                        Frequency = "ANNUAL",
                        StartDate = DateTime.Parse("01/01/2019"),
                        Status = Data.Models.Bussines.Enums.Status.Canceled,
                        Product = new Product()
                        {
                            Id = 5,
                            Idntfr = "LIFE5",
                            Label = "Life 5",
                            FrequencyRule = "MONTHLY",
                            MaxAge = 40,
                            MinAge = 18
                        },
                        Person = new Person()
                        {
                            Id = 49,
                            StartDate = DateTime.Parse("01/01/1996")
                        },
                        Distributor = new Distributor
                        {
                            Id = 50,
                            FullName = "Dist3"
                        }
                    }
                }
            };
        }


        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyDataProduct());
            context.AddRange(GetDummyDataPerson());
            context.AddRange(GetDummyDataContract());
            context.AddRange(GetDummyDataPremium());
            await context.SaveChangesAsync();
        }

        public PremiumServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }



        //PremiumServiceModel GetById(long id);
        [Fact]
        public async Task GetById_WithExistingPremium_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "PremiumServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.premiumService = new PremiumService(context, new ContractService(context));
            await SeedData(context);
            var actualResults = this.premiumService.GetById(1223);
            Assert.True(actualResults != null, errorMessagePrefix);
        }

        // DistributorServiceModel GetById(long id)
        [Fact]
        public async Task GetById_WithNotExistingPremium_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "DistributorServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.premiumService = new PremiumService(context, new ContractService(context));
            await SeedData(context);
            var actualResults = this.premiumService.GetById(5);
            Assert.True(actualResults == null, errorMessagePrefix);
        }
        //Task<bool> Create(PremiumServiceModel premiumServiceModel);
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "PremiumService Create(PremiumServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.premiumService = new PremiumService(context, new ContractService(context));
            await SeedData(context);
            PremiumServiceModel prem = new PremiumServiceModel()
            {
                Id = 14,
                ContractId = 2,
                OperationAmount = 100

            };

            var actualResults = await this.premiumService.Create(prem);
            var actualEntry = this.premiumService.GetById(14);
            var contract = context.Contracts.SingleOrDefault(c => c.Id == actualEntry.ContractId);
            Assert.True(prem.ContractId == actualEntry.ContractId, errorMessagePrefix + " " + "Contract is not returned properly.");
            Assert.True(prem.OperationAmount == actualEntry.OperationAmount, errorMessagePrefix + " " + "Contract is not returned properly.");
            Assert.True(HealthIns.Data.Models.Financial.Enums.Status.Pending == actualEntry.Status, errorMessagePrefix + " " + "Contract is not returned properly.");
            Assert.True(DateTime.Parse("01/01/2020").ToShortDateString() == contract.NextBillingDueDate.ToShortDateString(), errorMessagePrefix + " " + "Contract NextBillingDueDate is not calculated properly.");

        }
        //PremiumServiceModel SimulatePremiumForContract(long contractId);
        [Theory]
        [InlineData(200)]
        [InlineData(300)]
        [InlineData(400)]
        public async Task SimulatePremiumForContract_ShouldReturnCorrectResults(double amount)
        {
            string errorMessagePrefix = "PremiumService SimulatePremiumForContract(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.premiumService = new PremiumService(context, new ContractService(context));
            await SeedData(context);

            var contract = context.Contracts.SingleOrDefault(c => c.Id == 2);
            contract.PremiumAmount = amount;
            await context.SaveChangesAsync();
            var actualResults = this.premiumService.SimulatePremiumForContract(2);

            Assert.True(amount == actualResults.OperationAmount, errorMessagePrefix + " " + "OperationAmount is not returned properly.");
            Assert.True(actualResults.StartDate.ToShortDateString() == DateTime.Parse("01/01/2019").ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
            Assert.True(actualResults.EndDate.ToShortDateString() == DateTime.Parse("31/12/2019").ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");

        }
        //IQueryable<PremiumServiceModel> FindPremiumsByContractId(long id);
        [Fact]
        public async Task FindPremiumsByContractId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "PremiumService FindPremiumsByContractId(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.premiumService = new PremiumService(context, new ContractService(context));
            await SeedData(context);

            var actualResults = this.premiumService.FindPremiumsByContractId(4);

            Assert.True(actualResults.Count() == 1, errorMessagePrefix + " " + "OperationAmount is not returned properly.");

        }

    }
}

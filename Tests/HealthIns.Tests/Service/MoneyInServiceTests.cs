using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services;
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
    public class MoneyInServiceTests
    {
        private IMoneyInService moneyInService;
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

        private List<MoneyIn> GetDummyDataMoneyIn()
        {

            return new List<MoneyIn>()
            {
                new MoneyIn()
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
                new MoneyIn()
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
            context.AddRange(GetDummyDataMoneyIn());
            await context.SaveChangesAsync();
        }

        public MoneyInServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }





        // Task<bool> Create(MoneyInServiceModel moneyInServiceModel);
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "PremiumService Create(PremiumServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.moneyInService = new MoneyInService(context, new ContractService(context));
            await SeedData(context);
            MoneyInServiceModel moneyIn = new MoneyInServiceModel()
            {
                Id = 14,
                ContractId = 2,
                OperationAmount = 100

            };

            var actualResults = await this.moneyInService.Create(moneyIn);
            var actualEntry = context.MoneyIns.Include(m=>m.Contract).SingleOrDefault(m=>m.Id==14);
            var contract = context.Contracts.SingleOrDefault(c => c.Id == actualEntry.Contract.Id);
            Assert.True(moneyIn.ContractId == actualEntry.Contract.Id, errorMessagePrefix + " " + "Contract is not returned properly.");
            Assert.True(moneyIn.OperationAmount == actualEntry.OperationAmount, errorMessagePrefix + " " + "OperationAmount is not returned properly.");
            Assert.True(HealthIns.Data.Models.Financial.Enums.Status.Pending == actualEntry.Status, errorMessagePrefix + " " + "Status is not set properly.");

        }

        // IQueryable<MoneyInServiceModel> FindMoneyInsByContractId(long id);
             [Fact]
             public async Task FindMoneyInsByContractId_ShouldReturnCorrectResults()
             {
                 string errorMessagePrefix = "PremiumService FindMoneyInsByContractId(long id) method does not work properly.";
        
                 var context = HealthInsDbContextInMemoryFactory.InitializeContext();
                 this.moneyInService = new MoneyInService(context, new ContractService(context));
                 await SeedData(context);
        
                 var actualResults = this.moneyInService.FindMoneyInsByContractId(4);
        
                 Assert.True(actualResults.Count() == 1, errorMessagePrefix + " " + "Money in Not Found");
        
             }
        [Fact]
        public async Task FindMoneyInsByContractIdShouldReturnEmptyResult()
        {
            string errorMessagePrefix = "PremiumService FindMoneyInsByContractId(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.moneyInService = new MoneyInService(context, new ContractService(context));
            await SeedData(context);

            var actualResults = this.moneyInService.FindMoneyInsByContractId(45);

            Assert.True(!actualResults.Any(), errorMessagePrefix + " " + "Money in Is Found");

        }

    }
}

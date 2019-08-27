using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Bussines.Enums;
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
    public class ClaimActivityServiceTests
    {
        private IClaimActivityService claimActivityService;
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

        private List<ClaimActivity> GetDummyDataClaim()
        {

            return new List<ClaimActivity>()
            {
                new ClaimActivity()
                {
                    Id = 1223,
                    Contract = new Contract
                    {
                        Id = 3,
                        Frequency = "ANNUAL",
                        StartDate = DateTime.Parse("01/01/2019"),
                        Amount=20000,
                        PremiumAmount=200,
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
                new ClaimActivity()
                {
                    Id = 1224,
                    Contract = new Contract
                    {
                        Id = 4,
                        Frequency = "ANNUAL",
                        Amount=10000,
                        PremiumAmount=100,
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
            context.AddRange(GetDummyDataClaim());
            await context.SaveChangesAsync();
        }

        public ClaimActivityServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        // ClaimActivityServiceModel GetById(long id);
        [Fact]
        public async Task GetById_WithExistingClaimActivityt_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = " ClaimActivityService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.claimActivityService = new ClaimActivityService(context, new ContractService(context));
            await SeedData(context);
            var actualResults = this.claimActivityService.GetById(1223);
            Assert.True(actualResults != null, errorMessagePrefix);
        }
        [Fact]
        public async Task GetById_NotExistingClaimActivityt_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = " ClaimActivityService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.claimActivityService = new ClaimActivityService(context, new ContractService(context));
            await SeedData(context);
            var actualResults = this.claimActivityService.GetById(12345);
            Assert.True(actualResults == null, errorMessagePrefix);
        }
        // Task<bool> Create(ClaimActivityServiceModel claimServiceModel);
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ClaimActivityService Create(ClaimActivityServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.claimActivityService = new ClaimActivityService(context, new ContractService(context));
            await SeedData(context);
            ClaimActivityServiceModel claim = new ClaimActivityServiceModel()
            {
                Id = 14,
                ContractId = 2,
                ClaimDate = DateTime.Parse("01/01/2019"),
                OperationAmount = 100

            };

            var actualResults = await this.claimActivityService.Create(claim);
            var actualEntry = this.claimActivityService.GetById(14);
            Contract contract = context.Contracts.Include(c => c.Person).SingleOrDefault(p => p.Id == claim.ContractId);
            Person person =context.Persons.SingleOrDefault(p => p.Id == contract.Person.Id);
            Assert.True(claim.ContractId == actualEntry.ContractId, errorMessagePrefix + " " + "Contract is not returned properly.");
            Assert.True(claim.ClaimDate.ToShortDateString() == actualEntry.ClaimDate.ToShortDateString(), errorMessagePrefix + " " + "ClaimDate is not returned properly.");
            Assert.True(claim.OperationAmount == actualEntry.OperationAmount, errorMessagePrefix + " " + "OperationAmount is not returned properly.");
            Assert.True(HealthIns.Data.Models.Financial.Enums.Status.Pending == actualEntry.Status, errorMessagePrefix + " " + "ClaimActivity Status is not set properly.");
            Assert.True(Status.Canceled == contract.Status, errorMessagePrefix + " " + "Contract Status is not set properly.");
            Assert.True(person.EndDate.ToShortDateString() == actualEntry.ClaimDate.ToShortDateString(), errorMessagePrefix + " " + "Person EndDate is not set properly.");

        }


        // IQueryable<ClaimActivityServiceModel> FindClaimsActivityByContractId(long id);

        [Fact]
        public async Task FindClaimsActivityByContractId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ClaimActivityService FindClaimsActivityByContractId(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.claimActivityService = new ClaimActivityService(context, new ContractService(context));
            await SeedData(context);

            var actualResults = this.claimActivityService.FindClaimsActivityByContractId(4);

            Assert.True(actualResults.Count() == 1, errorMessagePrefix + " " + "ClaimActivity not found.");

        }

        // Task<bool> Validate(ClaimActivityServiceModel claimActivityServiceModel);
        [Fact]
        public async Task Validate_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ClaimActivityService Validate(ClaimActivityServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.claimActivityService = new ClaimActivityService(context, new ContractService(context));
            await SeedData(context);
            ClaimActivityServiceModel claim = new ClaimActivityServiceModel()
            {
                Id = 14,
                ContractId = 2,
                ClaimDate = DateTime.Parse("01/01/2019"),
                OperationAmount = 100

            };

            var actualResults = await this.claimActivityService.Create(claim);
            var actualResults2 = await this.claimActivityService.Validate(claim);
            var actualEntry = this.claimActivityService.GetById(14);
            Contract contract = context.Contracts.Include(c => c.Person).SingleOrDefault(p => p.Id == claim.ContractId);
            Person person = context.Persons.SingleOrDefault(p => p.Id == contract.Person.Id);
            Assert.True(claim.ContractId == actualEntry.ContractId, errorMessagePrefix + " " + "Contract is not returned properly.");
            Assert.True(claim.ClaimDate.ToShortDateString() == actualEntry.ClaimDate.ToShortDateString(), errorMessagePrefix + " " + "ClaimDate is not returned properly.");
            Assert.True(claim.OperationAmount == actualEntry.OperationAmount, errorMessagePrefix + " " + "OperationAmount is not returned properly.");
            Assert.True(HealthIns.Data.Models.Financial.Enums.Status.Paid == actualEntry.Status, errorMessagePrefix + " " + "ClaimActivity Status is not set properly.");
            Assert.True(Status.Canceled == contract.Status, errorMessagePrefix + " " + "Contract Status is not set properly.");
            Assert.True(person.EndDate.ToShortDateString() == actualEntry.ClaimDate.ToShortDateString(), errorMessagePrefix + " " + "Person EndDate is not set properly.");

        }
    }
}

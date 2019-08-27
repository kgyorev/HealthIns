using HealthIns.Data;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Tests.Common;
using HealthIns.Web.ViewModels.Organization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthIns.Tests.Service
{
    public class OrganizationServiceTest
    {
        private IOrganizationService organizationService;

        private List<Organization> GetDummyDataOrganization()
        {
            return new List<Organization>()
            {
                new Organization()
                {
                  Id=1,
                  Vat="123456",
                  EndDate=DateTime.Now,
                  StartDate=DateTime.Parse("01/01/1996"),
                  FullName="Org1",
                },
               new Organization()
                {
                  Id=2,
                  Vat="222222",
                  EndDate=DateTime.Now,
                  FullName="Org2",
                  StartDate=DateTime.Parse("01/01/1996")

                }
            };
        }


        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyDataOrganization());
            await context.SaveChangesAsync();
        }

        public OrganizationServiceTest()
        {
            MapperInitializer.InitializeMapper();
        }
        //  IQueryable<OrganizationServiceModel> GetAllOrganizations()
        [Fact]
        public async Task GetAllOrganizations_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "OrganizationService GetAllPersons() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.organizationService = new OrganizationService(context);

            List<OrganizationServiceModel> actualResults = await this.organizationService.GetAllOrganizations().ToListAsync();
            List<OrganizationServiceModel> expectedResults = GetDummyDataOrganization().To<OrganizationServiceModel>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "Egn is not returned properly.");
                Assert.True(expectedEntry.Vat == actualEntry.Vat, errorMessagePrefix + " " + "Vat is not returned properly.");
                Assert.True(expectedEntry.StartDate.ToShortDateString() == actualEntry.StartDate.ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
                Assert.True(expectedEntry.EndDate.ToShortDateString() == actualEntry.EndDate.ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");
              }
        }
        [Fact]
        public async Task GetAllOrganizations_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "OrganizationService GetAllPersons() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);

            List<OrganizationServiceModel> actualResults = await this.organizationService.GetAllOrganizations().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }



        // OrganizationServiceModel GetById(long id);
        [Fact]
        public async Task GetById_WithExistingOrganization_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "OrganizationServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);
            await SeedData(context);
            var actualResults = this.organizationService.GetById(1);
            Assert.True(actualResults != null, errorMessagePrefix);
        }

        // PersonServiceModel GetById(long id)
        [Fact]
        public async Task GetById_WithNotExistingOrganization_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "OrganizationServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);
            await SeedData(context);
            var actualResults = this.organizationService.GetById(5);
            Assert.True(actualResults == null, errorMessagePrefix);
        }
        // Task<bool> Create(OrganizationServiceModel organizationServiceModel);
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "OrganizationService Create(OrganizationServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);
            await SeedData(context);

            OrganizationServiceModel newOrg = new OrganizationServiceModel()
            {
                Id = 3,
                Vat = "1122222256",
                EndDate = DateTime.Now,
                FullName = "Org 22",
                StartDate = DateTime.Parse("01/01/1996")
            };
            var actualResults = await this.organizationService.Create(newOrg);
            var actualEntry = this.organizationService.GetById(3);
            Assert.True(newOrg.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(newOrg.Vat == actualEntry.Vat, errorMessagePrefix + " " + "Vat is not returned properly.");
            Assert.True(newOrg.StartDate.ToShortDateString() == actualEntry.StartDate.ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
            Assert.True(newOrg.EndDate.ToShortDateString() == actualEntry.EndDate.ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");
   }
        // Task<bool> Update(OrganizationServiceModel organizationServiceModel);
        [Fact]
        public async Task Update_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "OrganizationService Update(OrganizationServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);
            await SeedData(context);
            OrganizationServiceModel org = context.Organizations.First().To<OrganizationServiceModel>();

            org.Vat = "11222222133";
            org.EndDate = DateTime.Now.AddDays(2);
            org.FullName = "Org 678";
            org.StartDate = DateTime.Parse("01/01/1996").AddDays(3);


            var actualResults = await this.organizationService.Update(org);
            var actualEntry = this.organizationService.GetById(org.Id);
            Assert.True(org.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(org.Vat == actualEntry.Vat, errorMessagePrefix + " " + "Egn is not returned properly.");
            Assert.True(org.StartDate.ToShortDateString() == actualEntry.StartDate.ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
            Assert.True(org.EndDate.ToShortDateString() == actualEntry.EndDate.ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");
        }
        // IQueryable<OrganizationServiceModel> SearchOrganization(OrganizationSearchViewModel organizationSearchViewModel);
        [Fact]
        public async Task SearchPerson_ByVatOnly_ShouldReturnResults()
        {
            string errorMessagePrefix = "OrganizationService SearchOrganization(OrganizationSearchViewModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);

            await SeedData(context);

            OrganizationSearchViewModel organizationSearchViewModel = new OrganizationSearchViewModel()
            {
                Vat = "222222"
            };
            var actualResults = this.organizationService.SearchOrganization(organizationSearchViewModel);
            Assert.True(actualResults.FirstOrDefault().Id == 2, errorMessagePrefix);
        }
        [Fact]
        public async Task SearchOrganization_ByVatAndFullName_ShouldReturnResults()
        {
            string errorMessagePrefix = "OrganizationService SearchOrganization(OrganizationSearchViewModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);

            await SeedData(context);

            OrganizationSearchViewModel organizationSearchViewModel = new OrganizationSearchViewModel()
            {
                Vat = "222222",
                FullName= "Org2"
            };
            var actualResults = this.organizationService.SearchOrganization(organizationSearchViewModel);
            Assert.True(actualResults.FirstOrDefault().Id == 2, errorMessagePrefix);
        }
        [Fact]
        public async Task SearchPerson_ByFullNameOnly_ShouldReturnResults()
        {
            string errorMessagePrefix = "OrganizationService SearchOrganization(OrganizationSearchViewModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);

            await SeedData(context);

            OrganizationSearchViewModel organizationSearchViewModel = new OrganizationSearchViewModel()
            {
                FullName = "Org2"
            };
            var actualResults = this.organizationService.SearchOrganization(organizationSearchViewModel);
            Assert.True(actualResults.FirstOrDefault().Id == 2, errorMessagePrefix);
        }
        // PersonServiceModel VerifyEgn(string egn);
        [Fact]
        public async Task VerifyVat_ShouldReturnResults()
        {
            string errorMessagePrefix = "PersonService VerifyEgn(string egn) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);

            await SeedData(context);
            var actualResults = this.organizationService.VerifyVat("123456");
            Assert.True(actualResults.Id == 1, errorMessagePrefix);
        }
        [Fact]
        public async Task VerifyVat_ShouldNotReturnResults()
        {
            string errorMessagePrefix = "PersonService VerifyEgn(string egn) method does not work properly.";
            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.organizationService = new OrganizationService(context);
            await SeedData(context);
            var actualResults = this.organizationService.VerifyVat("7546456233");
            Assert.True(actualResults == null, errorMessagePrefix);
        }



    }
}

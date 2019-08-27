using HealthIns.Data;
using HealthIns.Data.Models;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Tests.Common;
using HealthIns.Web.ViewModels.Distributor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthIns.Tests.Service
{
    public class DistributorServiceTests
    {
        private IDistributorService distributorService;

        private List<Distributor> GetDummyDataDistributor()
        {



            var Org = new Organization()
            {
                Id=12,
                FullName="Org 1",
                Vat ="12324234",
                
            };
            var user = new HealthInsUser()
            {
                Id = "a1",
                FullName = "User 1",
                UserName = "user1"

            };


            return new List<Distributor>()
            {
                new Distributor()
                {
                  Id=12,
                  Organization=Org,
                  FullName ="Dist1",
                  User = user
                },
               new Distributor()
                {
                  Id=13,
                 Organization=Org,
                 FullName ="Dist2",
                 User = user,
                 
                }
            };
        }


        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyDataDistributor());
            await context.SaveChangesAsync();
        }

        public DistributorServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        // IQueryable<DistributorServiceModel> GetAllDistributors();
        //
        // DistributorServiceModel GetById(long id);
        [Fact]
        public async Task GetById_WithExistingDistributor_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "DistributorServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.distributorService = new DistributorService(context);
            await SeedData(context);
            var actualResults = this.distributorService.GetById(13);
            Assert.True(actualResults != null, errorMessagePrefix);
        }

        // DistributorServiceModel GetById(long id)
        [Fact]
        public async Task GetById_WithNotExistingDistributorn_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "DistributorServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.distributorService = new DistributorService(context);
            await SeedData(context);
            var actualResults = this.distributorService.GetById(5);
            Assert.True(actualResults == null, errorMessagePrefix);
        }




        //
        // Task<bool> Create(DistributorServiceModel distributorServiceModel);
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "DistributorService Create(DistributorServiceModel) method does not work properly.";
    
            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.distributorService = new DistributorService(context);
            await SeedData(context);
            var org = new Organization()
            {
                Id = 21,
                FullName = "Org 2",
                Vat = "1232423455",
    
            };
    
            var user = new HealthInsUser()
            {
                Id = "a2",
                FullName = "User 2",
                UserName = "user2"
    
            };
            await context.AddAsync(org);
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            DistributorServiceModel newDist = new DistributorServiceModel()
            {
                Id = 14,
                OrganizationId = org.Id,
                FullName = "Dist3",
                UserUserName = user.UserName,
    
            };

            var actualResults = await this.distributorService.Create(newDist);
            var actualEntry = this.distributorService.GetById(14);
            Assert.True(newDist.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(newDist.OrganizationId == actualEntry.OrganizationId, errorMessagePrefix + " " + "Organization is not returned properly.");
            Assert.True(actualEntry.Organization!=null, errorMessagePrefix + " " + "Organization is not returned properly.");
            Assert.True(newDist.UserUserName == actualEntry.UserUserName, errorMessagePrefix + " " + "User is not returned properly.");
            Assert.True(actualEntry.User != null, errorMessagePrefix + " " + "User is not returned properly.");
        }



        // Task<bool> Update(DistributorServiceModel distributorServiceModel);
        [Fact]
        public async Task Update_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "DistributorService Update(DistributorServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();

            this.distributorService = new DistributorService(context);
            await SeedData(context);
            var persons = context.Persons.ToList();
            var user2 = new HealthInsUser()
            {
                Id = "a155",
                FullName = "User 155",
                UserName = "user155"

            };
            var org2 = new Organization()
            {
                Id = 155,
                FullName = "Org 155",
                Vat = "123242342",

            };
            await context.AddAsync(org2);
            await context.AddAsync(user2);
            await context.SaveChangesAsync();
            DistributorServiceModel dist = context.Distributors.First().To<DistributorServiceModel>();

            dist.FullName = "Dist 123";
            dist.OrganizationId = 155;
            dist.UserUserName = "user155";


            var actualResults = await this.distributorService.Update(dist);
            var actualEntry = this.distributorService.GetById(dist.Id);
            Assert.True(dist.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(dist.OrganizationId == actualEntry.OrganizationId, errorMessagePrefix + " " + "Organization is not returned properly.");
            Assert.True(dist.UserUserName == dist.UserUserName, errorMessagePrefix + " " + "User is not returned properly.");
       }
        // IQueryable<DistributorServiceModel> SearchDistributor(DistributorSearchViewModel distributorSearchModel);
        [Fact]
        public async Task SearchPerson_ByDistributorId_ShouldReturnResults()
        {
            string errorMessagePrefix = "DistributorService SearchDistributor(DistributorSearchViewModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.distributorService = new DistributorService(context);

            await SeedData(context);

            DistributorSearchViewModel distributorSearchViewModel = new DistributorSearchViewModel()
            {
                ReferenceId="12",
                SearchBy="distributorId"
            };
            var actualResults = this.distributorService.SearchDistributor(distributorSearchViewModel);
            Assert.True(actualResults.FirstOrDefault().Id == 12, errorMessagePrefix);
        }
        [Fact]
        public async Task SearchPerson_ByUserName_ShouldReturnResults()
        {
            string errorMessagePrefix = "DistributorService SearchDistributor(DistributorSearchViewModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.distributorService = new DistributorService(context);

            await SeedData(context);

            DistributorSearchViewModel distributorSearchViewModel = new DistributorSearchViewModel()
            {
                ReferenceId = "user1",
                SearchBy = "userName"
            };
            var actualResults = this.distributorService.SearchDistributor(distributorSearchViewModel);
            Assert.True(actualResults.FirstOrDefault().Id == 12, errorMessagePrefix);
        }
        [Fact]
        public async Task SearchPerson_ByNoCriteria_ShouldReturnAllDistributors()
        {
            string errorMessagePrefix = "DistributorService SearchDistributor(DistributorSearchViewModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.distributorService = new DistributorService(context);

            await SeedData(context);

            DistributorSearchViewModel distributorSearchViewModel = new DistributorSearchViewModel()
            {
            };
            var actualResults = this.distributorService.SearchDistributor(distributorSearchViewModel);
            Assert.True(actualResults.Count()==2, errorMessagePrefix);
        }
    }
}

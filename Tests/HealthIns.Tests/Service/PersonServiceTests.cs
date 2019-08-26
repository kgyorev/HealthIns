using HealthIns.Data;
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
    public class PersonServiceTests
    {
        private IPersonService personService;

        private List<Person> GetDummyDataPerson()
        {
            return new List<Person>()
            {
                new Person()
                {
                  Id=1,
                  Egn="123456",
                  EndDate=DateTime.Now,
                  StartDate=DateTime.Parse("01/01/1996"),
                  FullName="Dragan",
                  Sex="Male",
                   Smoker=true,
                },
               new Person()
                {
                  Id=2,
                  Egn="222222",
                   EndDate=DateTime.Now,
                   FullName="Ivan",
                   Smoker=false,
                   Sex="Male",
                  StartDate=DateTime.Parse("01/01/1996")

                }
            };
        }


        private async Task SeedData(HealthInsDbContext context)
        {
            context.AddRange(GetDummyDataPerson());
            await context.SaveChangesAsync();
        }

        public PersonServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }


        //  IQueryable<PersonServiceModel> GetAllPersons()
        [Fact]
        public async Task GetAllPersons_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ContractService GetAllPersons() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.personService = new PersonService(context);

            List<PersonServiceModel> actualResults = await this.personService.GetAllPersons().ToListAsync();
            List<PersonServiceModel> expectedResults = GetDummyDataPerson().To<PersonServiceModel>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "Egn is not returned properly.");
                Assert.True(expectedEntry.Egn == actualEntry.Egn, errorMessagePrefix + " " + "Egn is not returned properly.");
                Assert.True(expectedEntry.StartDate.ToShortDateString() == actualEntry.StartDate.ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
                Assert.True(expectedEntry.Sex == actualEntry.Sex, errorMessagePrefix + " " + "Sex is not returned properly.");
                Assert.True(expectedEntry.EndDate.ToShortDateString() == actualEntry.EndDate.ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");
                Assert.True(expectedEntry.Smoker == actualEntry.Smoker, errorMessagePrefix + " " + "Smoker Type is not returned properly.");
            }
        }
        [Fact]
        public async Task GetAllPersons_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "ContractService  GetAllPerson() method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.personService = new PersonService(context);

            List<PersonServiceModel> actualResults = await this.personService.GetAllPersons().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }



        // PersonServiceModel GetById(long id);
        [Fact]
        public async Task GetById_WithExistingPerson_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "PersonServiceModel GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.personService = new PersonService(context);
            await SeedData(context);
            var actualResults = this.personService.GetById(1);
            Assert.True(actualResults != null, errorMessagePrefix);
        }

        // PersonServiceModel GetById(long id)
        [Fact]
        public async Task GetById_WithNotExistingContract_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "PersonService GetById(long id) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.personService = new PersonService(context);
            await SeedData(context);
            var actualResults = this.personService.GetById(5);
            Assert.True(actualResults == null, errorMessagePrefix);
        }
        // Task<bool> Create(PersonServiceModel personServiceModel);
        [Fact]
        public async Task Create_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ContractService Create(ContractServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.personService = new PersonService(context);
            await SeedData(context);

            PersonServiceModel newPerson = new PersonServiceModel()
            {
                Id = 3,
                Egn = "11222222",
                EndDate = DateTime.Now,
                FullName = "Ivan 2",
                Smoker = false,
                Sex = "Male",
                StartDate = DateTime.Parse("01/01/1996")
            };
            var actualResults = await this.personService.Create(newPerson);
            var actualEntry = this.personService.GetById(3);
            Assert.True(newPerson.FullName == actualEntry.FullName, errorMessagePrefix + " " + "Egn is not returned properly.");
            Assert.True(newPerson.Egn == actualEntry.Egn, errorMessagePrefix + " " + "Egn is not returned properly.");
            Assert.True(newPerson.StartDate.ToShortDateString() == actualEntry.StartDate.ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
            Assert.True(newPerson.Sex == actualEntry.Sex, errorMessagePrefix + " " + "Sex is not returned properly.");
            Assert.True(newPerson.EndDate.ToShortDateString() == actualEntry.EndDate.ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");
            Assert.True(newPerson.Smoker == actualEntry.Smoker, errorMessagePrefix + " " + "Smoker Type is not returned properly.");
        }
        // Task<bool> Update(PersonServiceModel personServiceModel);
        [Fact]
        public async Task Update_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "PersonService Update(PersonServiceModel) method does not work properly.";

            var context = HealthInsDbContextInMemoryFactory.InitializeContext();
            this.personService = new PersonService(context);
            await SeedData(context);
            PersonServiceModel person = context.Persons.First().To<PersonServiceModel>();

            person.Egn = "112222221";
            person.EndDate = DateTime.Now.AddDays(2);
            person.FullName = "Ivan 22";
            person.Smoker = true;
            person.Sex = "Female";
            person.StartDate = DateTime.Parse("01/01/1996").AddDays(3);


            var actualResults = await this.personService.Update(person);
            var actualEntry = this.personService.GetById(person.Id);
            Assert.True(person.FullName == actualEntry.FullName, errorMessagePrefix + " " + "Egn is not returned properly.");
            Assert.True(person.Egn == actualEntry.Egn, errorMessagePrefix + " " + "Egn is not returned properly.");
            Assert.True(person.StartDate.ToShortDateString() == actualEntry.StartDate.ToShortDateString(), errorMessagePrefix + " " + "StartDate is not returned properly.");
            Assert.True(person.Sex == actualEntry.Sex, errorMessagePrefix + " " + "Sex is not returned properly.");
            Assert.True(person.EndDate.ToShortDateString() == actualEntry.EndDate.ToShortDateString(), errorMessagePrefix + " " + "EndDate is not returned properly.");
            Assert.True(person.Smoker == actualEntry.Smoker, errorMessagePrefix + " " + "Smoker Type is not returned properly.");

        }
        // IQueryable<PersonServiceModel> SearchPerson(PersonSearchViewModel personSearchViewModel);
        // PersonServiceModel VerifyEgn(string egn);
    }
}


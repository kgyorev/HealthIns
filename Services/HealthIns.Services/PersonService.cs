using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Person;

namespace HealthIns.Services
{
    public class PersonService : IPersonService
    {
        private readonly HealthInsDbContext context;

        public PersonService(HealthInsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(PersonServiceModel personServiceModel)
        {
            Person person = AutoMapper.Mapper.Map<Person>(personServiceModel);
            context.Persons.Add(person);
            int result = await context.SaveChangesAsync();
            personServiceModel.Id = person.Id;
            return result > 0;
        }


        public IQueryable<PersonServiceModel> GetAllPersons()
        {
            return this.context.Persons.To<PersonServiceModel>();
        }


        public PersonServiceModel GetById(long id)
        {
            return this.context.Persons
                .To<PersonServiceModel>()
                .SingleOrDefault(person => person.Id == id);
        }

        public IQueryable<PersonServiceModel> SearchPerson(PersonSearchViewModel personSearchViewModel)
        {
            string egn = personSearchViewModel.Egn??"";
            string fullName = personSearchViewModel.FullName??"";

            IQueryable<PersonServiceModel> personAll;

            if (!egn.Equals("") && !fullName.Equals(""))
            {
                personAll = this.context.Persons.Where(p => p.Egn == egn&&p.FullName.Contains(fullName)).To<PersonServiceModel>();
            }
            else if (!egn.Equals("") && fullName.Equals(""))
            {
                personAll = this.context.Persons.Where(p => p.Egn == egn).To<PersonServiceModel>();
            }
            else if (egn.Equals("") && !fullName.Equals(""))
            {
                personAll = this.context.Persons.Where(p=> p.FullName.Contains(fullName)).To<PersonServiceModel>();
            }
            else
            {
                personAll = this.context.Persons.To<PersonServiceModel>();
            }
            return personAll;
        }

        public async Task<bool> Update(PersonServiceModel personServiceModel)
        {
            Person person = AutoMapper.Mapper.Map<Person>(personServiceModel);
            context.Update(person);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }

        public bool VerifyEgn(string egn)
        {
            return this.context.Persons.Where(person => person.Egn == egn).ToList().Any();
        }
    }
}

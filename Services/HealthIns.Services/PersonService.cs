using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;

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

        public async Task<bool> Update(PersonServiceModel personServiceModel)
        {
            Person person = AutoMapper.Mapper.Map<Person>(personServiceModel);
            context.Update(person);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}

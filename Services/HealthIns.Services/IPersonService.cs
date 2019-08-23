using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IPersonService
    {
        IQueryable<PersonServiceModel> GetAllPersons();

        PersonServiceModel GetById(long id);

        Task<bool> Create(PersonServiceModel personServiceModel);
        Task<bool> Update(PersonServiceModel personServiceModel);
        IQueryable<PersonServiceModel> SearchPerson(PersonSearchViewModel personSearchViewModel);
        bool VerifyEgn(string egn);
    }
}

using HealthIns.Services.Models;
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
    }
}

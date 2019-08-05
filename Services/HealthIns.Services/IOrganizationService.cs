using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IOrganizationService
    {
        Task<bool> Create(OrganizationServiceModel organizationServiceModel);
        IQueryable<OrganizationServiceModel> GetAllOrganizations();
        Task<bool> Update(OrganizationServiceModel organizationServiceModel);
    }
}

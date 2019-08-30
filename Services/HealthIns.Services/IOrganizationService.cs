using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IOrganizationService
    {
        OrganizationServiceModel GetById(long? id);
        Task<bool> Create(OrganizationServiceModel organizationServiceModel);
        IQueryable<OrganizationServiceModel> GetAllOrganizations();
        Task<bool> Update(OrganizationServiceModel organizationServiceModel);
        IQueryable<OrganizationServiceModel> SearchOrganization(OrganizationSearchViewModel organizationSearchViewModel);
        OrganizationServiceModel VerifyVat(string vat);
    }
}

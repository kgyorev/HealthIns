using HealthIns.Data;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly HealthInsDbContext context;

        public OrganizationService(HealthInsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(OrganizationServiceModel organizationServiceModel)
        {
            Organization org = AutoMapper.Mapper.Map<Organization>(organizationServiceModel);
            context.Organizations.Add(org);
            int result = await context.SaveChangesAsync();
            organizationServiceModel.Id = org.Id;
            return result > 0;
        }


        public IQueryable<OrganizationServiceModel> GetAllOrganizations()
        {
            return this.context.Organizations.To<OrganizationServiceModel>();
        }


        public OrganizationServiceModel GetById(long id)
        {
            return this.context.Organizations
                .To<OrganizationServiceModel>()
                .SingleOrDefault(org => org.Id == id);
        }

        public IQueryable<OrganizationServiceModel> SearchOrganization(OrganizationSearchViewModel organizationSearchViewModel)
        {

            string vat = organizationSearchViewModel.Vat??"";
            string fullName = organizationSearchViewModel.FullName??"";

            IQueryable<OrganizationServiceModel> organizationAll;

            if (!vat.Equals("") && !fullName.Equals(""))
            {
                organizationAll = this.context.Organizations.Where(p => p.Vat == vat && p.FullName.Contains(fullName)).To<OrganizationServiceModel>();
            }
            else if (!vat.Equals("") && fullName.Equals(""))
            {
                organizationAll = this.context.Organizations.Where(o => o.Vat == vat).To<OrganizationServiceModel>();
            }
            else if (vat.Equals("") && !fullName.Equals(""))
            {
                organizationAll = this.context.Organizations.Where(o => o.FullName.Contains(fullName)).To<OrganizationServiceModel>();
            }
            else
            {
                organizationAll = this.GetAllOrganizations();
            }
            return organizationAll;
        }

        public async Task<bool> Update(OrganizationServiceModel organizationServiceModel)
        {
            Organization org = AutoMapper.Mapper.Map<Organization>(organizationServiceModel);
            context.Update(org);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }
        public OrganizationServiceModel VerifyVat(string vat)
        {
            return this.context.Organizations.Where(org => org.Vat == vat).To<OrganizationServiceModel>().SingleOrDefault();
        }
    }
}

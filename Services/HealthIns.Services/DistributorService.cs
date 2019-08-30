using HealthIns.Data;
using HealthIns.Data.Models;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Distributor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public class DistributorService : IDistributorService
    {
        private readonly HealthInsDbContext context;

        public DistributorService(HealthInsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(DistributorServiceModel distributorServiceModel)
        {
            Distributor distributor = AutoMapper.Mapper.Map<Distributor>(distributorServiceModel);

            HealthInsUser user = this.context.Users.SingleOrDefault(p => p.UserName == distributorServiceModel.UserUserName);
            Organization org = this.context.Organizations.SingleOrDefault(p => p.Id == distributorServiceModel.OrganizationId);
            distributor.User = user;
            distributor.Organization = org;
            context.Distributors.Add(distributor);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }


        public IQueryable<DistributorServiceModel> GetAllDistributors()
        {
            return this.context.Distributors.To<DistributorServiceModel>();
        }


        public DistributorServiceModel GetById(long? id)
        {
            return this.context.Distributors.Include(d => d.User).Include(d=>d.Organization)
                .To<DistributorServiceModel>()
                .SingleOrDefault(distributor => distributor.Id == id);
        }

        public IQueryable<DistributorServiceModel> SearchDistributor(DistributorSearchViewModel distributorSearchModel)
        {
            string searchBy = distributorSearchModel.SearchBy;
            string referenceIdStr = distributorSearchModel.ReferenceId;
            if (referenceIdStr == null || referenceIdStr.Equals(""))
            {
                return this.context.Distributors.To<DistributorServiceModel>();
            }
            switch (searchBy)
            {
                case "userName": return this.context.Distributors.Include(d => d.User).Where(d => d.User.UserName == referenceIdStr).To<DistributorServiceModel>(); 
                case "organizationId": long.TryParse(distributorSearchModel.ReferenceId, out long referenceId); return this.context.Distributors.Include(d=>d.Organization).Where(d => d.Organization.Id == referenceId).To<DistributorServiceModel>();
                default: long.TryParse(distributorSearchModel.ReferenceId, out referenceId); return this.context.Distributors.Where(d => d.Id == referenceId).To<DistributorServiceModel>();
            }
        }

        public async Task<bool> Update(DistributorServiceModel distributorServiceModel)
        {
            Distributor distDB = this.context.Distributors.SingleOrDefault(p => p.Id == distributorServiceModel.Id);

            HealthInsUser user = this.context.Users.SingleOrDefault(p => p.UserName == distributorServiceModel.UserUserName);
            Organization org = this.context.Organizations.SingleOrDefault(p => p.Id == distributorServiceModel.OrganizationId);
            distDB.User = user;
            distDB.Organization = org;
            distDB.FullName = distributorServiceModel.FullName;
            context.Update(distDB);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}

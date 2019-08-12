﻿using HealthIns.Data;
using HealthIns.Data.Models;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            HealthInsUser user = this.context.Users.SingleOrDefault(p => p.UserName == distributorServiceModel.HealthInsUserUserName);
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


        public DistributorServiceModel GetById(long id)
        {
            return this.context.Distributors
                .To<DistributorServiceModel>()
                .SingleOrDefault(distributor => distributor.Id == id);
        }

        public async Task<bool> Update(DistributorServiceModel distributorServiceModel)
        {
            Distributor distributor = AutoMapper.Mapper.Map<Distributor>(distributorServiceModel);
            context.Update(distributor);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
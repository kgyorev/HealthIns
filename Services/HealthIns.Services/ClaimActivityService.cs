using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Bussines.Enums;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Services
{
    public class ClaimActivityService : IClaimActivityService
    {
        private readonly HealthInsDbContext context;
        private readonly IContractService contractService;

        public ClaimActivityService(HealthInsDbContext context, IContractService contractService)
        {
            this.context = context;
            this.contractService = contractService;
        }
        public async Task<bool> Create(ClaimActivityServiceModel claimActivityServiceModel)
        {
            ClaimActivity claim = AutoMapper.Mapper.Map<ClaimActivity>(claimActivityServiceModel);
            Contract contract = this.context.Contracts.Include(c=>c.Person).SingleOrDefault(p => p.Id == claimActivityServiceModel.ContractId);
            Person person = this.context.Persons.SingleOrDefault(p => p.Id == contract.Person.Id);
            contract.Status = Status.Canceled;
            person.EndDate = claimActivityServiceModel.ClaimDate;
            claim.Contract = contract;
            claim.RecordDate = DateTime.Now;
            claim.ClaimDate = claimActivityServiceModel.ClaimDate;
            claim.Status = HealthIns.Data.Models.Financial.Enums.Status.Pending;
            context.ClaimActivities.Add(claim);
            context.Update(contract);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }

        public IQueryable<ClaimActivityServiceModel> FindClaimsActivityByContractId(long id)
        {
            return context.ClaimActivities.Include(p => p.Contract).Where(p => p.Contract.Id == id).To<ClaimActivityServiceModel>();
        }


        public ClaimActivityServiceModel GetById(long id)
        {
            return context.ClaimActivities.Include(p => p.Contract).Where(c=>c.Id==id).To<ClaimActivityServiceModel>().SingleOrDefault();
        }


        public async Task<bool> Validate(ClaimActivityServiceModel claimActivityServiceModel)
        {
           var claim = context.ClaimActivities.Where(c=>c.Id== claimActivityServiceModel.Id).SingleOrDefault();
            claim.Status = HealthIns.Data.Models.Financial.Enums.Status.Paid;
            context.Update(claim);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Services
{
    public class MoneyInService : IMoneyInService
    {
        private readonly HealthInsDbContext context;

        public MoneyInService(HealthInsDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Create(MoneyInServiceModel moneyInServiceModel)
        {
            MoneyIn moneyIn = AutoMapper.Mapper.Map<MoneyIn>(moneyInServiceModel);
            Contract contract = this.context.Contracts.SingleOrDefault(p => p.Id == moneyInServiceModel.ContractId);
            moneyIn.Contract = contract;
            moneyIn.RecordDate = DateTime.Now;
            moneyIn.Status = HealthIns.Data.Models.Financial.Enums.Status.Pending;
            context.MoneyIns.Add(moneyIn);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }

        public IQueryable<MoneyInServiceModel> FindMoneyInsByContractId(long id)
        {
            return context.MoneyIns.Include(p => p.Contract).Include(m => m.Premium).Where(p => p.Contract.Id == id).To<MoneyInServiceModel>();
        }
    }
}

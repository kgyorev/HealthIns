using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IMoneyInService
    {
        Task<bool> Create(MoneyInServiceModel moneyInServiceModel);
        IQueryable<MoneyInServiceModel> FindMoneyInsByContractId(long id);
    }
}

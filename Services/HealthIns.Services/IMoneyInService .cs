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
        IQueryable<MoneyInServiceModel> GetAllMoneyInsForContract(Contract contract);

        MoneyInServiceModel GetById(long id);

        Task<bool> Create(MoneyInServiceModel moneyInServiceModel);
     //   Task<bool> Update(MoneyInServiceModel premiumServiceModel);

    }
}

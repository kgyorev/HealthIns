using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IPremiumService
    {
        IQueryable<PremiumServiceModel> GetAllPremiumsForContract(Contract contract);

        PremiumServiceModel GetById(long id);

        Task<bool> Create(PremiumServiceModel premiumServiceModel);
        Task<bool> Update(PremiumServiceModel premiumServiceModel);
        PremiumServiceModel SimulatePremiumForContract(long contractId);
    }
}

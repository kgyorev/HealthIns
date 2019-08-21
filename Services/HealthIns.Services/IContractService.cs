using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IContractService
    {
        IQueryable<ContractServiceModel> GetAllContracts();

        ContractServiceModel GetById(long id);

        Task<bool> Create(ContractServiceModel contractServiceModel);
        Task<bool> Update(ContractServiceModel contractServiceModel);
        double ReturnPremiumAmount(Contract contract);
        DateTime CalculateNextBillingDueDate(Contract contract);
        Task<bool> TryToApplyFinancial(long contractId);
        IQueryable<ContractServiceModel> SearchContract(ContractSearchViewModel contractSearchInputModel);
    }
}

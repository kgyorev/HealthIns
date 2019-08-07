using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Services.Models;

namespace HealthIns.Services
{
    public class PremiumService : IPremiumService
    {
        private readonly HealthInsDbContext context;
        private readonly IContractService contractService;

        public PremiumService(HealthInsDbContext context, IContractService contractService)
        {
            this.context = context;
            this.contractService = contractService;
        }
        public async Task<bool> Create(PremiumServiceModel premiumServiceModel)
        {
            Premium premium = AutoMapper.Mapper.Map<Premium>(premiumServiceModel);
            Contract contract = this.context.Contracts.SingleOrDefault(p => p.Id == premiumServiceModel.ContractId);
            premium.Contract = contract;
            context.Premiums.Add(premium);
            contract.NextBillingDueDate = this.contractService.CalculateNextBillingDueDate(contract);
            int result = await context.SaveChangesAsync();
            return result > 0;
        }

        public IQueryable<PremiumServiceModel> GetAllPremiumsForContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public PremiumServiceModel GetById(long id)
        {
            throw new NotImplementedException();
        }

        public PremiumServiceModel SimulatePremiumForContract(long contractId)
        {
          ContractServiceModel contractServiceModel =  this.contractService.GetById(contractId);
            Contract contract = AutoMapper.Mapper.Map<Contract>(contractServiceModel);

            PremiumServiceModel premium = new PremiumServiceModel()
            {
                OperationAmount = contract.PremiumAmount,
                StartDate = contract.NextBillingDueDate,
                Contract = contract,
                EndDate = this.contractService.CalculateNextBillingDueDate(contract).AddDays(-1),
            };

            return premium;
        }

        public Task<bool> Update(PremiumServiceModel premiumServiceModel)
        {
            throw new NotImplementedException();
        }
        private void TryToPay(Premium premium)
        {
        }
    }
}

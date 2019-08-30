using HealthIns.Data;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models;
using HealthIns.Data.Models.Financial;
using HealthIns.Web.ViewModels.Contract;
using HealthIns.Data.Models.Bussines.Enums;

namespace HealthIns.Services
{
    public class ContractService : IContractService
    {
        private readonly HealthInsDbContext context;
        private readonly IPremiumService premiumService;
        private readonly IMoneyInService moneyInService;

        public ContractService(HealthInsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ContractServiceModel contractServiceModel)
        {

            Contract contract = AutoMapper.Mapper.Map<Contract>(contractServiceModel);

            Product product = this.context.Products.SingleOrDefault(p => p.Idntfr == contractServiceModel.ProductIdntfr);
            Person person = this.context.Persons.SingleOrDefault(p => p.Id == contractServiceModel.PersonId);
            Distributor distributor = this.context.Distributors.SingleOrDefault(p => p.Id == contractServiceModel.DistributorId);
            contract.Product = product;
            contract.Person = person;
            contract.Distributor = distributor;
            contract.Status = Status.InForce;
            contract.CreationDate = DateTime.Now;
            contract.EndDate = contract.StartDate.AddYears(contract.Duration);
            var premiumAmount = this.ReturnPremiumAmount(contract);
            contract.PremiumAmount = premiumAmount;

            //contract.FrequencyRule = String.Join(" ", productServiceModel.FrequencyRule);

            context.Contracts.Add(contract);
            int result = await context.SaveChangesAsync();
            contractServiceModel.Id = contract.Id;
            return result > 0;
        }

        public async Task<bool> Update(ContractServiceModel contractServiceModel)
        {

            Contract contractDB = this.context.Contracts.Include(c=>c.Person).Include(c=>c.Product).SingleOrDefault(p => p.Id == contractServiceModel.Id);
            Distributor distributor = this.context.Distributors.SingleOrDefault(d => d.Id == contractServiceModel.DistributorId);
            contractDB.Distributor = distributor;
            contractDB.Frequency = contractServiceModel.Frequency;
            contractDB.Amount = contractServiceModel.Amount;
            contractDB.Duration = contractServiceModel.Duration;
            contractDB.NextBillingDueDate = contractServiceModel.NextBillingDueDate;
            contractDB.StartDate = contractServiceModel.StartDate;
            contractDB.EndDate = contractServiceModel.StartDate.AddYears(contractServiceModel.Duration);
            var premiumAmount = this.ReturnPremiumAmount(contractDB);
            contractDB.PremiumAmount = premiumAmount;
            context.Update(contractDB);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<ContractServiceModel> GetAllContracts()
        {

            var contracts = this.context.Contracts.Include(prod => prod.Product).Include(pers => pers.Person);
            var allContracts = contracts.To<ContractServiceModel>();
            return allContracts;
        }

        public ContractServiceModel GetById(long id)
        {
             var resultDB = this.context.Contracts.Include(c => c.Product).Include(c => c.Person).Include(c => c.Distributor).SingleOrDefault(contract => contract.Id == id);
             if (resultDB == null)
             {
                 return null;
             }
             var allContracts = resultDB.To<ContractServiceModel>();
             return allContracts;
        }

        public double ReturnPremiumAmount(Contract contract)
        {
            int age = contract.Person.GetAge(contract.StartDate) + 1;
            int frequencyPeriods = this.GetFrequencyPeriods(contract.Frequency);
            double premiumAmount = age * contract.Amount / (contract.Duration * 12 * 100) / frequencyPeriods;
            return Math.Round(premiumAmount * 100.0) / 100.0;
        }

        public DateTime CalculateNextBillingDueDate(Contract contract)
        {
            String frequency = contract.Frequency;
            int months = this.GetFrequencyMonths(frequency);
            return contract.NextBillingDueDate.AddMonths(months);
        }

        private int GetFrequencyMonths(String frequency)
        {
            switch (frequency)
            {
                case "ANUAL":
                    return 12;
                case "SEMI_ANNUAL":
                    return 6;
                case "TRIMESTER":
                    return 3;
                default:
                    return 1;
            }
        }
        private int GetFrequencyPeriods(String frequency)
        {
            switch (frequency)
            {
                case "ANUAL":
                    return 1;
                case "SEMI_ANNUAL":
                    return 2;
                case "TRIMESTER":
                    return 4;
                default:
                    return 12;
            }
        }

        public async Task<bool> TryToApplyFinancial(long contractId)
        {
            Premium pendingPremium =  this.context.Premiums.OrderBy(p=>p.StartDate).FirstOrDefault(p => p.Contract.Id == contractId &&p.Status== Data.Models.Financial.Enums.Status.Pending);
            MoneyIn pendingMoneyIn = this.context.MoneyIns.OrderBy(p => p.RecordDate).FirstOrDefault(p => p.Contract.Id == contractId && p.Status == Data.Models.Financial.Enums.Status.Pending);
            if(pendingPremium!=null&& pendingMoneyIn != null)
            {
                pendingPremium.Status = Data.Models.Financial.Enums.Status.Paid;
                pendingPremium.MoneyIn = pendingMoneyIn;
                pendingMoneyIn.Status = Data.Models.Financial.Enums.Status.Paid;
                int result = await context.SaveChangesAsync();
                return result > 0;
            }
           return false;
        }

        public IQueryable<ContractServiceModel> SearchContract(ContractSearchViewModel contractSearchInputModel)
        {
            string id = contractSearchInputModel.CntrctId??"";
            string status = contractSearchInputModel.Status??"";
            IQueryable<ContractServiceModel> allContractsViewModel;
            if (!id.Equals("") && !(status == ""))
            {
                Status statusParsed = (Status)Enum.Parse(typeof(Status), status);
                allContractsViewModel = this.context.Contracts.Include(prod => prod.Product).Include(pers => pers.Person).Where(c => c.Id == long.Parse(id) && c.Status == statusParsed).To<ContractServiceModel>();
            }
            else if (!id.Equals("") && status == "")
            {
                allContractsViewModel = this.context.Contracts.Include(prod => prod.Product).Include(pers => pers.Person).Where(c => c.Id == long.Parse(id)).To<ContractServiceModel>();
            }
            else if (id.Equals("") && !(status == ""))
            {
                Status statusParsed = (Status)Enum.Parse(typeof(Status), status);
                allContractsViewModel = this.context.Contracts.Include(prod => prod.Product).Include(pers => pers.Person).Where(c => c.Status == statusParsed).To<ContractServiceModel>();
            }
            else
            {
                allContractsViewModel = this.GetAllContracts();
            }
            return allContractsViewModel;
        }

        public IQueryable<ContractServiceModel> FindContractsByDistributorId(long id)
        {
            var allContracts = this.context.Contracts.Include(prod => prod.Product).Include(pers => pers.Person).Include(dist => dist.Distributor).Where(c => c.Distributor.Id==id).To<ContractServiceModel>();
            return allContracts;
        }

    }
}


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

namespace HealthIns.Services
{
    public class ContractService : IContractService
    {
        private readonly HealthInsDbContext context;

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

            var premiumAmount = this.ReturnPremiumAmount(contract);
            contract.PremiumAmount = premiumAmount;

            //contract.FrequencyRule = String.Join(" ", productServiceModel.FrequencyRule);

            context.Contracts.Add(contract);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Update(ContractServiceModel contractServiceModel)
        {

            Contract contract = AutoMapper.Mapper.Map<Contract>(contractServiceModel);

            Product product = this.context.Products.SingleOrDefault(p => p.Idntfr == contractServiceModel.ProductIdntfr);
            Person person = this.context.Persons.SingleOrDefault(p => p.Id == contractServiceModel.PersonId);
            Distributor distributor = this.context.Distributors.SingleOrDefault(d => d.Id == contractServiceModel.DistributorId);
            contract.Product = product;
            contract.Person = person;
            contract.Distributor = distributor;
            //contract.FrequencyRule = String.Join(" ", productServiceModel.FrequencyRule);

            var premiumAmount = this.ReturnPremiumAmount(contract);
            contract.PremiumAmount = premiumAmount;
            context.Update(contract);
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
            return this.context.Contracts
                .To<ContractServiceModel>()
                .SingleOrDefault(contract => contract.Id == id);
        }

        public double ReturnPremiumAmount(Contract contract)
        {
            int age = contract.Person.GetAge(contract.StartDate)+1;
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

    }
}


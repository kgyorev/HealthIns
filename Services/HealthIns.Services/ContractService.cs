using HealthIns.Data;
using HealthIns.Data.Models;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthIns.Data.Models.Enums;

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
            contract.Product = product;
            contract.Person = person;
            contract.Status = Status.InForce;
            contract.CreationDate = DateTime.UtcNow;
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
            contract.Product = product;
            contract.Person = person;
            //contract.FrequencyRule = String.Join(" ", productServiceModel.FrequencyRule);

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
    }
}


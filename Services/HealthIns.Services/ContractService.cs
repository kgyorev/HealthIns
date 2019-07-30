using HealthIns.Data;
using HealthIns.Data.Models;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Product product = this.context.Products.SingleOrDefault(p => p.Idntfr == contractServiceModel.ProductId);
            contract.Product = product;

            //contract.FrequencyRule = String.Join(" ", productServiceModel.FrequencyRule);

            context.Contracts.Add(contract);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }


        public IQueryable<ContractServiceModel> GetAllContracts()
        {
            return this.context.Contracts.To<ContractServiceModel>();
        }

        public ContractServiceModel GetById(long id)
        {
            return this.context.Contracts
                .To<ContractServiceModel>()
                .SingleOrDefault(contract => contract.Id == id);
        }
    }
}


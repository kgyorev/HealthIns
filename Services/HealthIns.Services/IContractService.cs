using HealthIns.Services.Models;
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
    }
}

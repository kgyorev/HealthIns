using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IClaimActivityService
    {

        ClaimActivityServiceModel GetById(long id);

        Task<bool> Create(ClaimActivityServiceModel claimServiceModel);
         IQueryable<ClaimActivityServiceModel> FindClaimsActivityByContractId(long id);
        Task<bool> Validate(ClaimActivityServiceModel claimActivityServiceModel);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Bussines.Contract;
using HealthIns.Web.InputModels.Financial;
using HealthIns.Web.ViewModels.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PremiumController : Controller
    {
        public const string PREMIUM_CREATED = "Premium was created";
        private readonly IPremiumService premiumService;
        private readonly IContractService contractService;

        public PremiumController(IPremiumService premiumService, IContractService contractService)
        {
            this.premiumService = premiumService;
            this.contractService = contractService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create(long id)
        {
            PremiumServiceModel premiumServiceModel =  this.premiumService.SimulatePremiumForContract(id);
            PremiumCreateInputModel premiumCreateInputModel = AutoMapper.Mapper.Map<PremiumCreateInputModel>(premiumServiceModel);
            premiumCreateInputModel.ContractId = id;
            return this.View(premiumCreateInputModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PremiumCreateInputModel premiumCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            PremiumServiceModel premiumServiceModel = AutoMapper.Mapper.Map<PremiumServiceModel>(premiumCreateInputModel);
            premiumServiceModel.ContractId = premiumCreateInputModel.Id;
            premiumServiceModel.Id = 0;
           await this.premiumService.Create(premiumServiceModel);
           await this.contractService.TryToApplyFinancial(premiumServiceModel.ContractId);
            this.TempData["info"] = String.Format(PREMIUM_CREATED);
            return this.Redirect($"/Contract/Details/{premiumServiceModel.ContractId}");
        }
    }
}
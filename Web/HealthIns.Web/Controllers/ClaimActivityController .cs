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
    public class ClaimActivityController : Controller
    {
        public const string CLAIM_CREATED = "Claim was created";
        public const string CLAIM_VALIDATED = "Claim was validated";
        private readonly IClaimActivityService claimActivityService;
        private readonly IContractService contractService;

        public ClaimActivityController(IClaimActivityService claimActivityService, IContractService contractService)
        {
            this.claimActivityService = claimActivityService;
            this.contractService = contractService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create(long id)
        {
            var contract = contractService.GetById(id);
            var claimActivityCreateInputModel = new ClaimActivityCreateInputModel()
            {
                OperationAmount = contract.Amount,
                ClaimDate=DateTime.Now,
                ContractId = id
            };
            return this.View(claimActivityCreateInputModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClaimActivityCreateInputModel claimActivityCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(claimActivityCreateInputModel);
            }
            ClaimActivityServiceModel claimActivityServiceModel = AutoMapper.Mapper.Map<ClaimActivityServiceModel>(claimActivityCreateInputModel);
            claimActivityServiceModel.ContractId = claimActivityCreateInputModel.Id;
            claimActivityServiceModel.Id = 0;
           await this.claimActivityService.Create(claimActivityServiceModel);
            this.TempData["info"] = String.Format(CLAIM_CREATED);
            return this.Redirect($"/Contract/Details/{claimActivityServiceModel.ContractId}");
        }
        [HttpGet(Name = "Validate")]
        public async Task<IActionResult> Validate(long id)
        {
            ClaimActivityServiceModel claimFromDB = this.claimActivityService.GetById(id);
            ClaimActivityCreateInputModel claim = claimFromDB.To<ClaimActivityCreateInputModel>();
            return this.View(claim);
        }
        [HttpPost]
        public async Task<IActionResult> Validate(ClaimActivityCreateInputModel claimActivityCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(claimActivityCreateInputModel);
            }
            ClaimActivityServiceModel claimActivityServiceModel = this.claimActivityService.GetById(claimActivityCreateInputModel.Id);
             await this.claimActivityService.Validate(claimActivityServiceModel);
            this.TempData["info"] = String.Format(CLAIM_VALIDATED);
            return this.Redirect($"/Contract/Details/{claimActivityServiceModel.ContractId}");
        }
    }
}
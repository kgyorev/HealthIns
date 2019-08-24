using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Data.Models.Bussines;
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
    public class MoneyInController : Controller
    {
        public const string MONEYIN_CREATED = "Money In was created";
        private readonly IMoneyInService moneyInService;
        private readonly IPremiumService premiumService;
        private readonly IContractService contractService;

        public MoneyInController(IContractService contractService,IMoneyInService moneyInService, IPremiumService premiumService)
        {
            this.contractService = contractService;
            this.moneyInService = moneyInService;
            this.premiumService = premiumService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create(long id)
        {
            var contract = this.contractService.GetById(id);
            MoneyInCreateInputModel moneyInCreateInputModel = new MoneyInCreateInputModel()
            {
                ContractId = id,
                OperationAmount=contract.PremiumAmount
            };
            return this.View(moneyInCreateInputModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoneyInCreateInputModel moneyInCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            MoneyInServiceModel moneyInServiceModel = AutoMapper.Mapper.Map<MoneyInServiceModel>(moneyInCreateInputModel);
            moneyInServiceModel.ContractId = moneyInCreateInputModel.Id;
            moneyInServiceModel.Id = 0;
            await this.moneyInService.Create(moneyInServiceModel);
            await this.contractService.TryToApplyFinancial(moneyInServiceModel.ContractId);
            this.TempData["info"] = String.Format(MONEYIN_CREATED);
            return this.Redirect($"/Contract/Details/{moneyInServiceModel.ContractId}");
        }
    }
}
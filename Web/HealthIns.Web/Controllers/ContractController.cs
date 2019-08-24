using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Bussines.Contract;
using HealthIns.Web.ViewModels.Contract;
using HealthIns.Web.ViewModels.MoneyIn;
using HealthIns.Web.ViewModels.Premium;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ContractController : Controller
    {
        public const string  CONTRACT_CREATED = "Contract with #{0} was created";
        public const string CONTRACT_UPDATED = "Contract with #{0} was updated";
        private readonly IContractService contractService;
        private readonly IProductService productService;
        private readonly IPremiumService premiumService;
        private readonly IMoneyInService moneyInService;

        public ContractController(IContractService contractService, IProductService productService, IPremiumService premiumService, IMoneyInService moneyInService)
        {
            this.contractService = contractService;
            this.productService = productService;
            this.premiumService = premiumService;
            this.moneyInService = moneyInService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContractCreateInputModel contractCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            ContractServiceModel contractServiceModel = AutoMapper.Mapper.Map<ContractServiceModel>(contractCreateInputModel);
            var output = this.productService.CheckProductRules(contractServiceModel);

            if (output.Any())
            {
                this.TempData["error"] = output.FirstOrDefault();
                return this.View();
            }
            await this.contractService.Create(contractServiceModel);
            this.TempData["info"] = String.Format(CONTRACT_CREATED, contractServiceModel.Id);
            return this.Redirect("/");
        }
        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(long Id)
        {
            ContractServiceModel contractFromDB = this.contractService.GetById(Id);
            ContractCreateInputModel contract = contractFromDB.To<ContractCreateInputModel>();
            return this.View(contract);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ContractCreateInputModel contractCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(contractCreateInputModel);
            }
            ContractServiceModel contractServiceModel = AutoMapper.Mapper.Map<ContractServiceModel>(contractCreateInputModel);
            var output = this.productService.CheckProductRules(contractServiceModel);
            if (output.Any())
            {
                this.TempData["error"] = output.FirstOrDefault();
                return this.View(contractCreateInputModel);
            }
            await this.contractService.Update(contractServiceModel);
            this.TempData["info"] = String.Format(CONTRACT_UPDATED, contractServiceModel.Id);
            return this.Redirect("/Contract/Search");
        }
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(ContractViewModel contractViewModel)
        {
            ContractServiceModel contractFromDB = this.contractService.GetById(contractViewModel.Id);
            List<PremiumServiceModel> premiumsForContractServiceModel = await this.premiumService.FindPremiumsByContractId(contractViewModel.Id).ToListAsync();
            List<PremiumViewModel> premiumsForContractViewModel = AutoMapper.Mapper.Map<List<PremiumViewModel>>(premiumsForContractServiceModel);
            List<MoneyInServiceModel> moneyInsForContractServiceModel = await this.moneyInService.FindMoneyInsByContractId(contractViewModel.Id).ToListAsync();
            List<MoneyInViewModel> moneyInsForContractViewModel = AutoMapper.Mapper.Map<List<MoneyInViewModel>>(moneyInsForContractServiceModel);
            ContractViewModel contract = contractFromDB.To<ContractViewModel>();
            contract.PremiumsFound = premiumsForContractViewModel;
            contract.MoneyInsFound = moneyInsForContractViewModel;
            contract.SelectedTab = contractViewModel.SelectedTab;
            return this.View(contract);
        }
        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(ContractSearchViewModel contractSearchInputModel)
        {
            List<ContractServiceModel> contractsFoundService = await this.contractService.SearchContract(contractSearchInputModel).ToListAsync();
            List<ContractViewModel> contractsFound = contractsFoundService
             .Select(d => d.To<ContractViewModel>()).ToList();
            contractSearchInputModel.ContractsFound = contractsFound;
            return this.View(contractSearchInputModel);
        }
    }
}
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    public class MoneyInController : Controller
    {

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

            // var contractId = long.Parse(id);
            // PremiumServiceModel premiumServiceModel =  this.premiumService.SimulatePremiumForContract(id);
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
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            MoneyInServiceModel moneyInServiceModel = AutoMapper.Mapper.Map<MoneyInServiceModel>(moneyInCreateInputModel);
            moneyInServiceModel.ContractId = moneyInCreateInputModel.Id;
            moneyInServiceModel.Id = 0;

           await this.moneyInService.Create(moneyInServiceModel);
           await this.contractService.TryToApplyFinancial(moneyInServiceModel.ContractId);


            return this.Redirect("/");
        }


        //[HttpGet(Name = "Edit")]
        //public async Task<IActionResult> Edit(long Id)
        //{

        //    ContractServiceModel contractFromDB = this.contractService.GetById(Id);

        //    ContractCreateInputModel contract = contractFromDB.To<ContractCreateInputModel>();

        //    return this.View(contract);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(ContractCreateInputModel contractCreateInputModel)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View();
        //        //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
        //    }


        //    ContractServiceModel contractServiceModel = AutoMapper.Mapper.Map<ContractServiceModel>(contractCreateInputModel);
        //    await this.contractService.Update(contractServiceModel);

        //    return this.Redirect("/");
        //}

        //[HttpGet(Name = "Search")]
        //public async Task<IActionResult> Search()
        //{

        //    List<ContractServiceModel> contractsFromDb = await this.contractService.GetAllContracts().ToListAsync();

        //    List<ContractViewModel> contractsAll = contractsFromDb
        //        .Select(contract => contract.To<ContractViewModel>()).ToList();

        //    return this.View(contractsAll);
        //}
    }
}
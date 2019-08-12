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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    public class PremiumController : Controller
    {

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

           // var contractId = long.Parse(id);
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
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            PremiumServiceModel premiumServiceModel = AutoMapper.Mapper.Map<PremiumServiceModel>(premiumCreateInputModel);
            premiumServiceModel.ContractId = premiumCreateInputModel.Id;
            premiumServiceModel.Id = 0;

           await this.premiumService.Create(premiumServiceModel);
           await this.contractService.TryToApplyFinancial(premiumServiceModel.ContractId);

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
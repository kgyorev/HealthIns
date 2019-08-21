using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Bussines.Contract;
using HealthIns.Web.ViewModels.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    public class ContractController : Controller
    {
        public const string  CONTRACT_CREATED = "Contract with #{0} was created";
        private readonly IContractService contractService;
        private readonly IProductService productService;

        public ContractController(IContractService contractService, IProductService productService)
        {
            this.contractService = contractService;
            this.productService = productService;
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
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
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
                return this.View();
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            ContractServiceModel contractServiceModel = AutoMapper.Mapper.Map<ContractServiceModel>(contractCreateInputModel);
            await this.contractService.Update(contractServiceModel);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(ContractSearchViewModel contractSearchInputModel)
        {


            List<ContractServiceModel> contractsFoundService = await this.contractService.SearchContract(contractSearchInputModel).ToListAsync();
            List<ContractViewModel> contractsFound = contractsFoundService
             .Select(d => d.To<ContractViewModel>()).ToList();
            contractSearchInputModel.ContractsFound = contractsFound;
            return this.View(contractSearchInputModel);



          //  List<ContractServiceModel> contractsFromDb = await this.contractService.GetAllContracts().ToListAsync();
          //
          //  List<ContractViewModel> contractsAll = contractsFromDb
          //      .Select(contract => contract.To<ContractViewModel>()).ToList();
          //
          //  return this.View(contractsAll);
        }
    }
}
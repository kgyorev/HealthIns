using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthIns.Web.Controllers
{
    [Area("Bussines")]
    public class ContractController : Controller
    {

        private readonly IContractService contractService;

        public ContractController(IContractService contractService)
        {
            this.contractService = contractService;
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

            await this.contractService.Create(contractServiceModel);

            return this.Redirect("/");
        }
    }
}
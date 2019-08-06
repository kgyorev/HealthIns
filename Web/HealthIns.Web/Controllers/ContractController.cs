﻿using System;
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
        public async Task<IActionResult> Search()
        {

            List<ContractServiceModel> contractsFromDb = await this.contractService.GetAllContracts().ToListAsync();

            List<ContractViewModel> contractsAll = contractsFromDb
                .Select(contract => contract.To<ContractViewModel>()).ToList();

            return this.View(contractsAll);
        }
    }
}
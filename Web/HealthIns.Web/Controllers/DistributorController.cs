using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;

using HealthIns.Web.InputModels.Bussines.Distributor;
using HealthIns.Web.ViewModels.Contract;
using HealthIns.Web.ViewModels.Distributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class DistributorController : Controller
    {

        private readonly IDistributorService distributorService;
        private readonly IContractService contractService;

        public DistributorController(IDistributorService distributorService, IContractService contractService)
        {
            this.distributorService = distributorService;
            this.contractService = contractService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {

            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(DistributorCreateInputModel distributorCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }


            DistributorServiceModel distributorServiceModel = AutoMapper.Mapper.Map<DistributorServiceModel>(distributorCreateInputModel);
            await this.distributorService.Create(distributorServiceModel);
            return this.Redirect("/");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            DistributorServiceModel distributorFromDB = this.distributorService.GetById(Id);

            DistributorCreateInputModel distributor = distributorFromDB.To<DistributorCreateInputModel>();

            return this.View(distributor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DistributorCreateInputModel distributorCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }


            DistributorServiceModel distributorServiceModel = AutoMapper.Mapper.Map<DistributorServiceModel>(distributorCreateInputModel);

            await this.distributorService.Update(distributorServiceModel);
           // this.TempData["info"] = String.Format(UPDATED_Distributor, distributorServiceModel.Id);
            return this.Redirect("/");
        }



        [HttpGet]
        public async Task<IActionResult> Details(DistributorViewModel distributorViewModel)
        {
            DistributorServiceModel distributorFromDB = this.distributorService.GetById(distributorViewModel.Id);

            List<ContractServiceModel> contractsForDistributorServiceModel = await this.contractService.FindContractsByDistributorId(distributorViewModel.Id).ToListAsync();

            List<ContractViewModel> contractsForDistributorViewModel = AutoMapper.Mapper.Map<List<ContractViewModel>>(contractsForDistributorServiceModel);

            DistributorViewModel distributor =distributorFromDB.To<DistributorViewModel>();
            distributor.ContractsFound = contractsForDistributorViewModel;

            distributor.SelectedTab = distributorViewModel.SelectedTab;
            return this.View(distributor);
        }

        [HttpGet(Name = "Search")]
       public async Task<IActionResult> Search(DistributorSearchViewModel distributorSearchModel)
       {

            //  var distributorSearchModel = new DistributorSearchInputModel()
            //  {
            //      ReferenceId = "",
            //      SearchBy = "distributorId"
            //  };

            List<DistributorServiceModel> distributorsFoundService=  await this.distributorService.SearchDistributor(distributorSearchModel).ToListAsync();
            List<DistributorViewModel> distributorsFound = distributorsFoundService
             .Select(d => d.To<DistributorViewModel>()).ToList();
           distributorSearchModel.DistributorsFound = distributorsFound;
           return this.View(distributorSearchModel);
       }
    }
}
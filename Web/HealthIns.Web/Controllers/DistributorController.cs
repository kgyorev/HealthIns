using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;

using HealthIns.Web.InputModels.Bussines.Distributor;
using HealthIns.Web.ViewModels.Distributor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    public class DistributorController : Controller
    {

        private readonly IDistributorService distributorService;

        public DistributorController(IDistributorService distributorService)
        {
            this.distributorService = distributorService;
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
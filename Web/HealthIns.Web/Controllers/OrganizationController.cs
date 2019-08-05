using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.PersOrg;
using Microsoft.AspNetCore.Mvc;

namespace HealthIns.Web.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {

            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrganizationCreateInputModel organizationCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
      
      
            OrganizationServiceModel organizationServiceModel = AutoMapper.Mapper.Map<OrganizationServiceModel>(organizationCreateInputModel);
      
            await this.organizationService.Create(organizationServiceModel);
      
            return this.Redirect("/");
        }

        //  [HttpGet(Name = "Search")]
        //  public async Task<IActionResult> Search()
        //  {
        //
        //      List<ContractServiceModel> contractsFromDb = await this.personService.GetAllPersons().ToListAsync();
        //
        //      List<PersonViewModel> personsAll = contractsFromDb
        //          .Select(contract => contract.To<PersonViewModel>()).ToList();
        //
        //      return this.View(personsAll);
        //  }
    }
}
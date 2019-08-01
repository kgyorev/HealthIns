using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
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


      //  [HttpPost]
      //  public async Task<IActionResult> Create(PersonCreateInputModel personCreateInputModel)
      //  {
      //      if (!this.ModelState.IsValid)
      //      {
      //          return this.View();
      //          //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
      //      }
      //
      //
      //      PersonServiceModel personServiceModel = AutoMapper.Mapper.Map<PersonServiceModel>(personCreateInputModel);
      //
      //      await this.personService.Create(personServiceModel);
      //
      //      return this.Redirect("/");
      //  }

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
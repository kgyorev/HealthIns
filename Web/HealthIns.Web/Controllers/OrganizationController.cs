using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.PersOrg;
using HealthIns.Web.ViewModels.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class OrganizationController : Controller
    {
        private const string ORG_VAT_EXISTS = "There is already Organization with this Vat!";
        private const string CREATED_ORG = "Organization with Id# {0} was created";
        private const string UPDATED_ORG = "Organization with Id# {0} was updated";
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

            if (this.organizationService.VerifyVat(organizationCreateInputModel.Vat))
            {
                this.TempData["error"] = ORG_VAT_EXISTS;
                return this.View();
            }
            OrganizationServiceModel organizationServiceModel = AutoMapper.Mapper.Map<OrganizationServiceModel>(organizationCreateInputModel);
      
            await this.organizationService.Create(organizationServiceModel);
            this.TempData["info"] = String.Format(CREATED_ORG, organizationServiceModel.Id);
            return this.Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            OrganizationServiceModel organizationFromDB = this.organizationService.GetById(Id);

            OrganizationCreateInputModel org = organizationFromDB.To<OrganizationCreateInputModel>();

            return this.View(org);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrganizationCreateInputModel organizationCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            OrganizationServiceModel organizationServiceModel = AutoMapper.Mapper.Map<OrganizationServiceModel>(organizationCreateInputModel);

            await this.organizationService.Update(organizationServiceModel);
            this.TempData["info"] = String.Format(UPDATED_ORG, organizationServiceModel.Id);
            return this.Redirect("/");
        }

        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(OrganizationSearchViewModel organizationSearchViewModel)
        {
            List<OrganizationServiceModel> organizationsFoundService = await this.organizationService.SearchOrganization(organizationSearchViewModel).ToListAsync();
            List<OrganizationViewModel> organizationsFound = organizationsFoundService
             .Select(d => d.To<OrganizationViewModel>()).ToList();
            organizationSearchViewModel.OrganizationsFound = organizationsFound;
            return this.View(organizationSearchViewModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.PersOrg;
using HealthIns.Web.ViewModels.Organization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
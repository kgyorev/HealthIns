using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.PersOrg;
using HealthIns.Web.ViewModels.Person;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {

            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateInputModel personCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            PersonServiceModel personServiceModel = AutoMapper.Mapper.Map<PersonServiceModel>(personCreateInputModel);

            await this.personService.Create(personServiceModel);

            return this.Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            PersonServiceModel personFromDB = this.personService.GetById(Id);

            PersonCreateInputModel person = personFromDB.To<PersonCreateInputModel>();

            return this.View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PersonCreateInputModel personCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
                //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            PersonServiceModel personServiceModel = AutoMapper.Mapper.Map<PersonServiceModel>(personCreateInputModel);

            await this.personService.Update(personServiceModel);

            return this.Redirect("/");
        }



        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(PersonSearchViewModel personSearchViewModel)
        {



            List<PersonServiceModel> personsFoundService = await this.personService.SearchPerson(personSearchViewModel).ToListAsync();
            List<PersonViewModel> personsFound = personsFoundService
             .Select(d => d.To<PersonViewModel>()).ToList();
            personSearchViewModel.PersonsFound = personsFound;
            return this.View(personSearchViewModel);
        }
    }
}
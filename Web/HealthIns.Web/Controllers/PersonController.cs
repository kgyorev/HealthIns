﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.PersOrg;
using HealthIns.Web.ViewModels.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Controllers
{

    [Authorize(Roles = "Admin,User")]
    public class PersonController : Controller
    {
        private const string PERSON_EGN_EXISTS = "There is already Person with this EGN!";
        private const string CREATED_PERSON = "Person with Id# {0} was created";
        private const string UPDATED_PERSON = "Person with Id# {0} was updated";
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
            if (this.personService.VerifyEgn(personCreateInputModel.Egn))
            {
                this.TempData["error"] = PERSON_EGN_EXISTS;
                return this.View();
            }

            PersonServiceModel personServiceModel = AutoMapper.Mapper.Map<PersonServiceModel>(personCreateInputModel);

            await this.personService.Create(personServiceModel);
            this.TempData["info"] = String.Format(CREATED_PERSON, personServiceModel.Id);
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
            this.TempData["info"] = String.Format(UPDATED_PERSON, personServiceModel.Id);
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

        [HttpGet]
        public async Task<IActionResult> Details(long Id)
        {
            PersonServiceModel personFromDB = this.personService.GetById(Id);

            PersonCreateInputModel person = personFromDB.To<PersonCreateInputModel>();

            return this.View(person);
        }
    }
}
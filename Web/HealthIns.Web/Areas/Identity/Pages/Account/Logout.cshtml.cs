﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HealthIns.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthIns.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private const string LOGOUT_OK = "Logut Successful!";
        private readonly SignInManager<HealthInsUser> _signInManager;

        public LogoutModel(SignInManager<HealthInsUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
            this.TempData["info"] = String.Format(LOGOUT_OK);
            return Redirect("/Identity/Account/Login");
        }
    }
}
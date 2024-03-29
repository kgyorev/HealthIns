﻿using HealthIns.Data;
using HealthIns.Services;
using HealthIns.Web.InputModels.Bussines.Contract;
using HealthIns.Web.InputModels.Bussines.Distributor;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class UserExistingValidatorAttribute : ValidationAttribute
    {
        private const string ERROR = "User with this Username not exist!";

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            DistributorCreateInputModel distributorEntry = (DistributorCreateInputModel)validationContext.ObjectInstance;
             var _context = (HealthInsDbContext)validationContext
                          .GetService(typeof(HealthInsDbContext));
               var user = _context.Users.FirstOrDefault(c => c.UserName == distributorEntry.UserUserName);
            if (user == null)
            {
                return new ValidationResult(ERROR);
            }
            else
            {
                return ValidationResult.Success;
            }
            }
        }
    }



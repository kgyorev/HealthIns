using HealthIns.Data;
using HealthIns.Services;
using HealthIns.Web.InputModels.Bussines.Contract;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class PersonExistingValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            ContractCreateInputModel contractEntry = (ContractCreateInputModel)validationContext.ObjectInstance;
            var _personService = (IPersonService)validationContext
             .GetService(typeof(IPersonService));
            var person = _personService.GetById(contractEntry.PersonId);
            if (person == null)
            {
                return new ValidationResult("Person with this Id not exist!");
            }
            else
            {
                return ValidationResult.Success;
            }
            }
        }
    }



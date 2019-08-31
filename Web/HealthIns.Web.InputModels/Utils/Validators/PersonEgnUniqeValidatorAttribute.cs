using HealthIns.Services;
using HealthIns.Web.InputModels.PersOrg;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class PersonEgnUniqeValidatorAttribute : ValidationAttribute
    {
        private const string ERROR = "There is Person with this Egn, Egn should be uniqe!";

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            PersonCreateInputModel personEntry = (PersonCreateInputModel)validationContext.ObjectInstance;
            var _personService = (IPersonService)validationContext
             .GetService(typeof(IPersonService));
           var person= _personService.GetById(personEntry.Id);
            var personOther = _personService.VerifyEgn(personEntry.Egn);
            if (personOther == null)
            {
                return ValidationResult.Success;
            }
            else if ((person != null&&person.Id != personOther.Id && personOther.Egn == personEntry.Egn)||(person == null && personOther.Egn == personEntry.Egn))
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



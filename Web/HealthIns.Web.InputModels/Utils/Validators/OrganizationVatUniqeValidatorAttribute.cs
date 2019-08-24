using HealthIns.Services;
using HealthIns.Web.InputModels.PersOrg;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class OrganizationVatUniqeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            OrganizationCreateInputModel orgEntry = (OrganizationCreateInputModel)validationContext.ObjectInstance;
            var _organizationService = (IOrganizationService)validationContext
             .GetService(typeof(IOrganizationService));
           var org= _organizationService.GetById(orgEntry.Id);
            var orgOther = _organizationService.VerifyVat(orgEntry.Vat);
            if (orgOther == null)
            {
                return ValidationResult.Success;
            }
            else if ((org != null&& org.Id != orgOther.Id && orgOther.Vat == orgEntry.Vat)||(org == null && orgOther.Vat == orgEntry.Vat))
            {
                return new ValidationResult("There is Organization with this Vat, Vat should be uniqe!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}



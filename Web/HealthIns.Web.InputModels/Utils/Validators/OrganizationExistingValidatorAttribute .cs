using HealthIns.Data;
using HealthIns.Services;
using HealthIns.Web.InputModels.Bussines.Distributor;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class OrganizationExistingValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
             object value, ValidationContext validationContext)
        {
            DistributorCreateInputModel distributorEntry = (DistributorCreateInputModel)validationContext.ObjectInstance;
           var _organizationService = (IOrganizationService)validationContext
             .GetService(typeof(IOrganizationService));
            var org = _organizationService.GetById(distributorEntry.OrganizationId);
            if (org == null)
            {
                return new ValidationResult("Organization with this Id not exist!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}



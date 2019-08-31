using HealthIns.Data;
using HealthIns.Services;
using HealthIns.Web.InputModels.Bussines.Contract;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class DistributorExistingValidatorAttribute : ValidationAttribute
    {
        private const string ERROR = "Distributor with this Id not exist!";

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            ContractCreateInputModel contractEntry = (ContractCreateInputModel)validationContext.ObjectInstance;
            // var _context = (HealthInsDbContext)validationContext
            //              .GetService(typeof(HealthInsDbContext));
            //   var distributor = _context.Distributors.FirstOrDefault(c => c.Id == contractEntry.DistributorId);
            var _distributorService = (IDistributorService)validationContext
             .GetService(typeof(IDistributorService));
            var distributor =  _distributorService.GetById(contractEntry.DistributorId);
            if (distributor == null)
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



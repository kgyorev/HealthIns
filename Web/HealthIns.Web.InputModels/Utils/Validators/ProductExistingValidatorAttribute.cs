using HealthIns.Data;
using HealthIns.Services;
using HealthIns.Web.InputModels.Bussines.Contract;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class ProductExistingValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            ContractCreateInputModel contractEntry = (ContractCreateInputModel)validationContext.ObjectInstance;
            var _productService = (IProductService)validationContext
             .GetService(typeof(IProductService));
            var product = _productService.GetByIdntfr(contractEntry.ProductIdntfr);
            if (product == null)
            {
                return new ValidationResult("Product with this Identifier not exist!");
            }
            else
            {
                return ValidationResult.Success;
            }
            }
        }
    }



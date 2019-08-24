using HealthIns.Services;
using HealthIns.Web.InputModels.Bussines;
using HealthIns.Web.InputModels.PersOrg;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace HealthIns.Web.InputModels.Utils.Validators
{
    public class ProductIdntfrUniqeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            ProductCreateInputModel productEntry = (ProductCreateInputModel)validationContext.ObjectInstance;
            var _productService = (IProductService)validationContext
             .GetService(typeof(IProductService));
           var product= _productService.GetById(productEntry.Id);
            var productOther = _productService.GetByIdntfr(productEntry.Idntfr);
            if (productOther == null)
            {
                return ValidationResult.Success;
            }
            else if ((product != null&& product.Id != productOther.Id && productOther.Idntfr == productEntry.Idntfr)||(product == null && productOther.Idntfr == productEntry.Idntfr))
            {
                return new ValidationResult("There is Product with this Identifier, Identifier should be uniqe!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}



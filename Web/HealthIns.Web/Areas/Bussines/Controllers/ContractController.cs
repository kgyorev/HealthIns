using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Web.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthIns.Web.Controllers
{
    [Area("Bussines")]
    public class ContractController : Controller
    {
        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {

            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ContractCreateInputModel contractCreateInputModel)
        {
            //  ProductServiceModel productServiceModel = new ProductServiceModel
            //  {
            //      Name = productCreateInputModel.Name,
            //      Price = productCreateInputModel.Price,
            //      ManufacturedOn = productCreateInputModel.ManufacturedOn,
            //      ProductType = new ProductTypeServiceModel
            //      {
            //          Name = productCreateInputModel.ProductType
            //      }
            //  };
            //
            //  await this.productService.Create(productServiceModel);

            return this.Redirect("/");
        }
    }
}
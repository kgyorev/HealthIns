using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Bussines;
using Microsoft.AspNetCore.Mvc;

namespace HealthIns.Web.Areas.Bussines.Controllers
{
    [Area("Bussines")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {

            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel productCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
             //   return this.View(productCreateInputModel ?? new ProductCreateInputModel());
            }


            ProductServiceModel productServiceModel = AutoMapper.Mapper.Map<ProductServiceModel>(productCreateInputModel);

                await this.productService.Create(productServiceModel);

            return this.Redirect("/");
        }
    }
}
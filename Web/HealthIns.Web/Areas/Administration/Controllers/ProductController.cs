using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Models;
using HealthIns.Web.Areas.Administration.Controllers;
using HealthIns.Web.InputModels.Bussines;
using Microsoft.AspNetCore.Mvc;

namespace HealthIns.Web.Areas.Bussines.Controllers
{
    public class ProductController : AdminController
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
            }


            ProductServiceModel productServiceModel = AutoMapper.Mapper.Map<ProductServiceModel>(productCreateInputModel);

            string frequencyRule = string.Join(",",productCreateInputModel.FrequencyRule);
            productServiceModel.FrequencyRule = frequencyRule;
            await this.productService.Create(productServiceModel);

            return this.Redirect("/");
        }
    }
}
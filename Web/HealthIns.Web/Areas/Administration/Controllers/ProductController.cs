using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.Areas.Administration.Controllers;
using HealthIns.Web.InputModels.Bussines;
using HealthIns.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthIns.Web.Areas.Bussines.Controllers
{
    public class ProductController : AdminController
    {
        private readonly IProductService productService;
        public const string PRODUCT_CREATED = "Product with #{0} was created";
        public const string PRODUCT_UPDATED = "Contract with #{0} was updated";

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
            this.TempData["info"] = String.Format(PRODUCT_CREATED, productServiceModel.Id);
            return this.Redirect("/");
        }

        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(long Id)
        {

            ProductServiceModel productFromDB = this.productService.GetById(Id);
            ProductCreateInputModel product = productFromDB.To<ProductCreateInputModel>();
            var FrequencyList = product.FrequencyRule = productFromDB.FrequencyRule.Split(",").ToList();
            product.FrequencyRule = FrequencyList;
            return this.View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateInputModel productCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            ProductServiceModel productServiceModel = AutoMapper.Mapper.Map<ProductServiceModel>(productCreateInputModel);
            string frequencyRule = string.Join(",", productCreateInputModel.FrequencyRule);
            productServiceModel.FrequencyRule = frequencyRule;
            await this.productService.Update(productServiceModel);
            this.TempData["info"] = String.Format(PRODUCT_UPDATED, productServiceModel.Id);
            return this.Redirect("/Administration/Product/Search");
        }


        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(ProductSearchViewModel productSearchInputModel)
        {
            List<ProductServiceModel> productsFoundService = await this.productService.SearchProduct(productSearchInputModel).ToListAsync();
            List<ProductViewModel> productsFound = productsFoundService
             .Select(d => d.To<ProductViewModel>()).ToList();
            productSearchInputModel.ProductsFound = productsFound;
            return this.View(productSearchInputModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long Id)
        {
            ProductServiceModel productFromDB = this.productService.GetById(Id);
            ProductCreateInputModel product = productFromDB.To<ProductCreateInputModel>();
            var FrequencyList = product.FrequencyRule = productFromDB.FrequencyRule.Split(",").ToList();
            product.FrequencyRule = FrequencyList;
            return this.View(product);
        }
    }
}
using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
  public  class ProductService : IProductService
    {
        private const string ERROR_FREQUENCY_ALLOWED = "Please fill the form correctly, allowed frequencies for this product are ";
        private const string ERROR_MAXIMUM_AGE = "Please fill the form correctly, at contract start date Owner is {0} years old, maximum allowed age for this product is {1}!";
        private const string ERROR_MINIMUM_AGE = "Please fill the form correctly, at contract start date Owner is {0} years old, minimum allowed age for this product is {1}!";
        private readonly HealthInsDbContext context;

        public ProductService(HealthInsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ProductServiceModel productServiceModel)
        {

            Product product = AutoMapper.Mapper.Map<Product>(productServiceModel);
            product.FrequencyRule = String.Join(" ", productServiceModel.FrequencyRule);
            context.Products.Add(product);
            productServiceModel.Id = product.Id;
            int result = await context.SaveChangesAsync();

            return result > 0;
        }


        public IQueryable<ProductServiceModel> GetAllProducts()
        {
            return this.context.Products.To<ProductServiceModel>();
        }

        public ProductServiceModel GetById(long id)
        {
            return this.context.Products
                .To<ProductServiceModel>()
                .SingleOrDefault(product => product.Id == id);
        }

        public List<string> CheckProductRules(ContractServiceModel contract)
        {
            var productId = contract.ProductIdntfr;
            var product =  this.context.Products.SingleOrDefault(p => p.Idntfr == productId);


            DateTime startDt = contract.StartDate;
            List<string> output = new List<string>();
            if (!product.FrequencyRule.Contains(contract.Frequency.ToString()))
            {
                string[] freqRule = product.FrequencyRule.Replace('_','-').Split(",");
                string concat = "";
                foreach (var s in freqRule)
                {
                    if (freqRule[freqRule.Length - 1].Equals(s))
                    {
                        concat += s.First().ToString().ToUpper() + String.Join("", s.ToLower().Skip(1)) + "!";
                    }
                    else
                        concat += s.First().ToString().ToUpper() + String.Join("", s.ToLower().Skip(1)) + ", ";
                }
                output.Add(ERROR_FREQUENCY_ALLOWED + concat);
            }
            var person = this.context.Persons.SingleOrDefault(p => p.Id == contract.PersonId);
            int age = person.GetAge(startDt);
            int maxAge = product.MaxAge;
            int minAge = product.MinAge;
            if (age > maxAge)
            {
                output.Add(String.Format(ERROR_MAXIMUM_AGE, age, maxAge));
            }
            if (age < minAge)
            {
                output.Add(String.Format(ERROR_MINIMUM_AGE, age, minAge));
            }
            return output;
        }

        public IQueryable<ProductServiceModel> SearchProduct(ProductSearchViewModel productSearchInputModel)
        {
            string idntfr = productSearchInputModel.Idntfr??"";
            IQueryable<ProductServiceModel> allProductsViewModel;
            if (!idntfr.Equals(""))
            {
                allProductsViewModel = this.context.Products.Where(p=>p.Idntfr.Contains(idntfr)).To<ProductServiceModel>();
            }
            else
            {
                allProductsViewModel = this.GetAllProducts();
            }
            return allProductsViewModel;
        }

        public async Task<bool> Update(ProductServiceModel productServiceModel)
        {

           // Product product = AutoMapper.Mapper.Map<Product>(productServiceModel);
            Product productFromDb = await this.context.Products.SingleOrDefaultAsync(product => productServiceModel.Id == product.Id);

            productFromDb.FrequencyRule = productServiceModel.FrequencyRule;
            productFromDb.Idntfr = productServiceModel.Idntfr;
            productFromDb.Label = productServiceModel.Label;
            productFromDb.MaxAge = productServiceModel.MaxAge;
            productFromDb.MinAge = productServiceModel.MinAge;
            context.Update(productFromDb);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public ProductServiceModel GetByIdntfr(string productIdntfr)
        {
            return this.context.Products
                .To<ProductServiceModel>()
                .SingleOrDefault(product => product.Idntfr == productIdntfr);
        }
    }
}

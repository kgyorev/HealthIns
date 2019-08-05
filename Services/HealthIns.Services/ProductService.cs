using HealthIns.Data;
using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
  public  class ProductService : IProductService
    {
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
    }
}

﻿using HealthIns.Services.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IProductService
    {
        IQueryable<ProductServiceModel> GetAllProducts();

        ProductServiceModel GetById(long id);

        Task<bool> Create(ProductServiceModel productServiceModel);
    }
}
﻿using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIns.Services
{
    public interface IDistributorService
    {
        IQueryable<DistributorServiceModel> GetAllDistributors();

        DistributorServiceModel GetById(long id);

        Task<bool> Create(DistributorServiceModel distributorServiceModel);
        Task<bool> Update(DistributorServiceModel distributorServiceModel);
    }
}

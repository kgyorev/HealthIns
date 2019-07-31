using AutoMapper;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Contract
{
   public class ContractViewModel : IMapFrom<ContractServiceModel>
    {
        public long Id { get; set; }
        public string ProductIdntfr { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ContractServiceModel,ContractViewModel>();
     
        }

    }
}

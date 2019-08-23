using AutoMapper;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Product
{
   public class ProductViewModel : IMapFrom<ProductServiceModel>
    {
        public long Id { get; set; }
        public string Idntfr { get; set; }
        public string Label { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ProductServiceModel, ProductViewModel>();
                //.ForMember(destination => destination.PersonFullName,
                //            opts => opts.MapFrom(origin => origin.Person.FullName)); 
     
        }

    }
}

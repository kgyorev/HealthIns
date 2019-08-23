using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.ViewModels.Product
{
   public class ProductSearchViewModel
    {
        public string Idntfr { get; set; }
        public string Status { get; set; }
        public List<ProductViewModel> ProductsFound { get; set; }


        public ProductSearchViewModel()
        {
            this.ProductsFound = new List<ProductViewModel>();
        }


    }
}

using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.ViewModels.Distributor
{
   public class DistributorSearchViewModel 
    {
        public string SearchBy { get; set; }
        public string ReferenceId { get; set; }
        public List<DistributorViewModel> DistributorsFound { get; set; }


        public DistributorSearchViewModel()
        {
            this.DistributorsFound = new List<DistributorViewModel>();
        }


    }
}

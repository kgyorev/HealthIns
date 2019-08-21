using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.ViewModels.Distributor
{
   public class DistributorViewModel : IMapFrom<DistributorServiceModel>
    {
        public long Id { get; set; }
        public string HealthInsUserUserName { get; set; }
        public long OrganizationId { get; set; }
        public string FullName { get; set; }
    }
}

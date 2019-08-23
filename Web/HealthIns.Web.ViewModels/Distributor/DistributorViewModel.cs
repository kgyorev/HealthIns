using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.ViewModels.Distributor
{
   public class DistributorViewModel : IMapFrom<DistributorServiceModel>
    {
        public DistributorViewModel()
        {
            this.SelectedTab = "summary";
            this.ContractsFound = new List<ContractViewModel>();
        }

        public long Id { get; set; }
        public string UserUserName { get; set; }
        public long OrganizationId { get; set; }
        public string FullName { get; set; }
        public List<ContractViewModel> ContractsFound { get; set; }
        public string SelectedTab { get; set; }
    }
}

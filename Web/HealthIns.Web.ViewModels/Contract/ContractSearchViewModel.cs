using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.ViewModels.Contract
{
   public class ContractSearchViewModel
    {
        public string CntrctId { get; set; }
        public string Status { get; set; }
        public List<ContractViewModel> ContractsFound { get; set; }


        public ContractSearchViewModel()
        {
            this.Status = "";
            this.CntrctId = "";
            this.ContractsFound = new List<ContractViewModel>();
        }


    }
}

using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.ViewModels.Contract
{
   public class ContractSearchViewModel : SearchPagenationRoot
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

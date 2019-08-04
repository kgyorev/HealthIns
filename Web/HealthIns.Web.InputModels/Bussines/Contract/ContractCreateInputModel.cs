using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthIns.Web.InputModels
{
    public class ContractCreateInputModel: IMapFrom<ContractServiceModel>, IMapTo<ContractServiceModel>
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "Product Identifyer is required!")]
        public string ProductIdntfr { get; set; }
        public long PersonId { get; set; }

        [Required(ErrorMessage = "Frequency is required!")]
        public string Frequency { get; set; }

        public int Amount { get; set; }

        public int Duration { get; set; }

        public DateTime NextBillingDueDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}

using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Contract
{
    public class ContractDetailsViewModel : IMapFrom<ContractServiceModel>
    {
        public long Id { get; set; }
        public string ProductIdntfr { get; set; }
        public string Frequency { get; set; }
        public int Amount { get; set; }
        public int Duration { get; set; }
        public DateTime NextBillingDueDate { get; set; }
        public DateTime StartDate { get; set; }

        public string PersonId { get; set; }
    }
}

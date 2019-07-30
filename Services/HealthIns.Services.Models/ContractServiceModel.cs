
using HealthIns.Data.Models;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;

namespace HealthIns.Services.Models
{
    public class ContractServiceModel : IMapFrom<Contract>, IMapTo<Contract>
    {
        public long Id { get; set; }
        public string ProductId { get; set; }
        public string Frequency { get; set; }
        public int Amount { get; set; }
        public int Duration { get; set; }
        public DateTime NextBillingDueDate { get; set; }
        public DateTime StartDate { get; set; }

    }
}

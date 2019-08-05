using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;

namespace HealthIns.Services.Models
{
    public class ContractServiceModel : IMapFrom<Contract>, IMapTo<Contract>
    {
        public long Id { get; set; }
        public string ProductIdntfr { get; set; }
        public string Frequency { get; set; }
        public double Amount { get; set; }
        public int Duration { get; set; }
        public DateTime NextBillingDueDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Status Status { get; set; }
        public double PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }

        public Distributor Distributor { get; set; }
        public long DistributorId { get; set; }
        public Person Person { get; set; }
        public long PersonId { get; set; }


   




    }
}

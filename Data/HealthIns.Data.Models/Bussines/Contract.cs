using HealthIns.Data.Models.Bussines.Enums;
using HealthIns.Data.Models.PrsnOrg;
using System;

namespace HealthIns.Data.Models.Bussines
{
   public class Contract : BaseModel<long>
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Frequency { get; set; }
        public Status Status { get; set; }
        public double Amount { get; set; }
        public double PremiumAmount { get; set; }
        public int Duration { get; set; }
        public DateTime NextBillingDueDate { get; set; }
        public Product Product { get; set; }
        public Distributor Distributor { get; set; }
        public Person Person { get; set; }
    }
}

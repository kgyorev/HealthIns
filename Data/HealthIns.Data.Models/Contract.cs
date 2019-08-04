using HealthIns.Data.Models.Enums;
using HealthIns.Data.Models.PrsnOrg;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models
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
        public Person Person { get; set; }
    }
}

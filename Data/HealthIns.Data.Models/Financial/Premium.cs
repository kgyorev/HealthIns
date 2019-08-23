using HealthIns.Data.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.Financial
{
   public class Premium : FinancialBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MoneyIn MoneyIn { get; set; }
        public long? MoneyInId { get; set; }

    }
}

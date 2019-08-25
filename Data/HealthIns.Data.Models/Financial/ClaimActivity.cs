using HealthIns.Data.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.Financial
{
   public class ClaimActivity : FinancialBase
    {
        public DateTime ClaimDate { get; set; }
        public string ClaimEventType { get; set;}
    }
}

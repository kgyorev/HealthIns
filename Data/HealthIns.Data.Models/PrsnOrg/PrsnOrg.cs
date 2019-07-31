using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.PrsnOrg
{
   public abstract class PrsnOrg : BaseModel<long>
    {
        public string FullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
     
    }
}

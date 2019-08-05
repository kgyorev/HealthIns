using HealthIns.Data.Models.PrsnOrg;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.Bussines
{
    public class Distributor : BaseModel<long>
    {
        public string FullName { get; set; }
        public Organization Organization { get; set; }
        public HealthInsUser User { get; set; }
    }
}

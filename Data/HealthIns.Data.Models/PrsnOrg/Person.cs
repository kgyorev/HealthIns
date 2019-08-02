using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.PrsnOrg
{
    public class Person : PrsnOrg
    {
        public string Egn { get; set; }
        public string Sex { get; set; }
        public bool Smoker { get; set; }
    }
}

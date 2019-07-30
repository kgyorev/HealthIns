using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models
{
   public class Contract
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Product Product { get; set; }
    }
}

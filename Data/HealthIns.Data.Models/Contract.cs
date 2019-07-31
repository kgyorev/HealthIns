using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models
{
   public class Contract : BaseModel<long>
    {
      //  public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Frequency { get; set; }
        public Product Product { get; set; }
    }
}

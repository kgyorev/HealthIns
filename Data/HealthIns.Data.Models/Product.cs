using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models
{
    public class Product : BaseModel<long>
    { 
        //public long Id { get; set; }
        public string Idntfr { get; set; }
        public string Label { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string FrequencyRule { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.Bussines
{
    public class Product : BaseModel<long>
    { 
        public string Idntfr { get; set; }
        public string Label { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string FrequencyRule { get; set; }
    }
}

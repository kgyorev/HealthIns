
using HealthIns.Data.Models.Bussines;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;

namespace HealthIns.Services.Models
{
    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public long Id { get; set; }
        public string Idntfr { get; set; }
        public string Label { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
       // public List<string> FrequencyRule { get; set; }
        public string FrequencyRule { get; set; }
    }
}

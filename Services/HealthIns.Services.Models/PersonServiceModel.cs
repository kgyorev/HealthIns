using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Services.Models
{
   public class PersonServiceModel: IMapFrom<Person>, IMapTo<Person>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Egn { get; set; }
        public string Sex { get; set; }
        public bool Smoker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

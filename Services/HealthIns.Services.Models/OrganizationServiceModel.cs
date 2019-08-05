using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Services.Models
{
   public class OrganizationServiceModel: IMapFrom<Organization>, IMapTo<Organization>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Vat { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

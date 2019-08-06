using HealthIns.Data.Models;
using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Services.Models
{
   public class DistributorServiceModel: IMapFrom<Distributor>, IMapTo<Distributor>
    {
        public long Id { get; set; }
        public string FullName { get; set; }

        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public HealthInsUser HealthInsUser { get; set; }
        public string HealthInsUserUserName { get; set; }

    }
}

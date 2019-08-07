using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.PrsnOrg;
using HealthIns.Services.Mapping;
using System;
using System.Collections.Generic;

namespace HealthIns.Services.Models
{
    public class MoneyInServiceModel : IMapFrom<MoneyIn>, IMapTo<MoneyIn>
    {
        public DateTime RecordDate { get; set; }
        public Status Status { get; set; }
        public double OperationAmount { get; set; }

        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Contract Contract { get; set; }
        public long ContractId { get; set; }

    }
}

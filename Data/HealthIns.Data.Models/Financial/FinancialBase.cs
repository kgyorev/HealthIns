using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.Financial
{
   public abstract class FinancialBase :BaseModel<long>
    {
        public DateTime RecordDate { get; set; }
        public Enums.Status Status { get; set; }
        public double OperationAmount { get; set; }
        public Contract Contract { get; set; }
    }
}

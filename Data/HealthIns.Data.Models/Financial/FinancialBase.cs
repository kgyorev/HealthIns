using HealthIns.Data.Models.Financial.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.Financial
{
   public abstract class FinancialBase :BaseModel<long>
    {
        public DateTime RecordDate { get; set; }
        public Status Status { get; set; }
        public double OperationAmount { get; set; }

        protected FinancialBase()
        {
            RecordDate = DateTime.UtcNow;
            Status = Status.Pending;
        }
    }
}

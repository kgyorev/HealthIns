using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.Financial.Enums;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthIns.Web.ViewModels.MoneyIn
{
    public class MoneyInViewModel: IMapFrom<MoneyInServiceModel>
    {

        public DateTime RecordDate { get; set; }
        public Status Status { get; set; }
        public double OperationAmount { get; set; }

        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long ContractId { get; set; }
        public long PremiumId { get; set; }


    }
}

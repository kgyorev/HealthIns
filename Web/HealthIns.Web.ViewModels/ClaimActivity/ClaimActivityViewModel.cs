using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.Financial.Enums;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthIns.Web.ViewModels.Premium
{
    public class ClaimActivityViewModel : IMapFrom<ClaimActivityServiceModel>
    {
        public long Id { get; set; }
        public DateTime RecordDate { get; set; }
        public Status Status { get; set; }
        public double OperationAmount { get; set; }
        public string ClaimEventType { get; set; }
        public DateTime ClaimDate { get; set; }
        public long ContractId { get; set; }

    }
}

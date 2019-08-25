using HealthIns.Data.Models.Bussines;
using HealthIns.Data.Models.Financial;
using HealthIns.Data.Models.Financial.Enums;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthIns.Web.InputModels.Financial
{
    public class ClaimActivityCreateInputModel: IMapFrom<ClaimActivityServiceModel>, IMapTo<ClaimActivityServiceModel>
    {

        public DateTime RecordDate { get; set; }
        public Status Status { get; set; }
        [Required]
        public double OperationAmount { get; set; }
        public string ClaimEventType { get; set; }
        public long Id { get; set; }
        public DateTime ClaimDate { get; set; }
        public long ContractId { get; set; }

    }
}

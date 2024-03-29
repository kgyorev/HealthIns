﻿using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Utils.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthIns.Web.InputModels.Bussines.Contract
{
    public class ContractCreateInputModel: IMapFrom<ContractServiceModel>, IMapTo<ContractServiceModel>
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "Product Identifyer is required!")]
        [ProductExistingValidator]
        public string ProductIdntfr { get; set; }
        [Required(ErrorMessage = "Person(Owner) Id is required!")]
        [PersonExistingValidator]
        public long? PersonId { get; set; }
        [Required(ErrorMessage = "Distributor Id is required!")]
        [DistributorExistingValidator]
        public long? DistributorId { get; set; }

        [Required(ErrorMessage = "Frequency is required!")]
        public string Frequency { get; set; }

        [Required(ErrorMessage = "Amount is required!")]
        [Range(0,1000000, ErrorMessage = "Amount should be between 0 and 1000000!")]
        public double? Amount { get; set; }

        [Required(ErrorMessage = "Contract Duration is required!")]
        public int? Duration { get; set; }

        [Required(ErrorMessage = "Next Billing Due Date is required!")]
        public DateTime? NextBillingDueDate { get; set; }

        [Required(ErrorMessage = "Contract Start Date is required!")]
        public DateTime? StartDate { get; set; }
    }
}

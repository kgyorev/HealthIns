﻿using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Utils.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.InputModels.Bussines.Distributor
{
   public class DistributorCreateInputModel : IMapFrom<DistributorServiceModel>, IMapTo<DistributorServiceModel>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "User Name is required!")]
        [UserExistingValidator]
        public string UserUserName { get; set; }
        [Required(ErrorMessage = "Organization Id is required!")]
        [OrganizationExistingValidator]
        public long? OrganizationId { get; set; }
        [Required(ErrorMessage = "Full Name is required!")]
        public string FullName { get; set; }
    }
}

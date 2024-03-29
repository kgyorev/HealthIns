﻿using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Organization
{
   public class OrganizationViewModel : IMapFrom<OrganizationServiceModel>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Vat { get; set; }
        public DateTime StartDate { get; set; }
    }
}

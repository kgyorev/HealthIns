using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Utils.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.InputModels.PersOrg
{
    public class OrganizationCreateInputModel : IMapFrom<OrganizationServiceModel>, IMapTo<OrganizationServiceModel>
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "Full Name is required!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vat is required!")]
        [OrganizationVatUniqeValidator]
        [MaxLength(10, ErrorMessage = "VAT Length should be 10")]
        [MinLength(10, ErrorMessage = "VAT Length should be 10")]
        public string Vat { get; set; }
        [Required(ErrorMessage = "Start Date is required!")]
        public DateTime? StartDate { get; set; }

    }
}

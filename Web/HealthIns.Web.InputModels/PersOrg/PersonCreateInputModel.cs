using HealthIns.Services;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Utils.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.InputModels.PersOrg
{
    public class PersonCreateInputModel : IMapFrom<PersonServiceModel>, IMapTo<PersonServiceModel>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Full Name is required!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "EGN is required!")]
        [PersonEgnUniqeValidator]
        public string Egn { get; set; }

        public string Smoker { get; set; }
        [Required(ErrorMessage = "Gender is required!")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Birth Date is required!")]
        public DateTime StartDate { get; set; }

    }
}

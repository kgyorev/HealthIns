using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Data.Models.Bussines.Enums
{
   public enum Status
    {
        [Display(Name = "In Force")]
        InForce = 1,

        [Display(Name = "Canceled")]
        Canceled = 2
    }
}


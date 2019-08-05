using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Data.Models.Financial.Enums
{
   public enum Status
    {
        [Display(Name = "Pending")]
        Pending = 1,
        [Display(Name = "Paid")]
        Paid = 2,
        [Display(Name = "Canceled")]
        Canceled = 3
    }
}


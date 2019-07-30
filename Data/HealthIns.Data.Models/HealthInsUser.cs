using Microsoft.AspNetCore.Identity;
using System;

namespace HealthIns.Data.Models
{
    public class HealthInsUser : IdentityUser
    {
        public HealthInsUser()
        {
        }
        public string FullName { get; set; }
    }
}

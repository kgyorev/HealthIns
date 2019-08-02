using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Person
{
    public class PersonViewModel : IMapFrom<PersonServiceModel>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Egn { get; set; }
        public string Smoker { get; set; }
        public string Sex { get; set; }
        public DateTime StartDate { get; set; }
    }
}

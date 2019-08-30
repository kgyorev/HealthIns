using HealthIns.Web.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Person
{
   public class PersonSearchViewModel : SearchPagenationRoot
    {
        public String Egn { get; set; }
        public String FullName { get; set; }
        public List<PersonViewModel> PersonsFound { get; set; }


        public PersonSearchViewModel()
        {
            this.Egn = "";
            this.FullName = "";
            this.PersonsFound = new List<PersonViewModel>();
        }
    }
}

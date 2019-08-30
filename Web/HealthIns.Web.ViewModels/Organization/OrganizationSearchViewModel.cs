using HealthIns.Web.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Organization
{
   public class OrganizationSearchViewModel : SearchPagenationRoot
    {
        public String Vat { get; set; }
        public String FullName { get; set; }
        public List<OrganizationViewModel> OrganizationsFound { get; set; }


        public OrganizationSearchViewModel()
        {
            this.Vat = "";
            this.FullName = "";
            this.OrganizationsFound = new List<OrganizationViewModel>();
        }
    }
}

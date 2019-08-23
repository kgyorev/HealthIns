using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Data.Models.PrsnOrg
{
    public class Person : PrsnOrg
    {
        public string Egn { get; set; }
        public string Sex { get; set; }
        public bool Smoker { get; set; }



        public int GetAge()
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            if (DateTime.Now <= this.StartDate)
            {
                return 0;
            }
            TimeSpan span = DateTime.Now - this.StartDate;
            int years = (zeroTime + span).Year - 1;
            return years;
        }

        public int GetAge(DateTime currentDate)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            if(currentDate<= this.StartDate)
            {
                return 0;
            }
            TimeSpan span = currentDate - this.StartDate;
            int years = (zeroTime + span).Year - 1;
            return years;
        }
    }
}

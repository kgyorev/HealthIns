using HealthIns.Data.Models.PrsnOrg;


namespace HealthIns.Data.Models.Bussines
{
    public class Distributor : BaseModel<long>
    {
        public string FullName { get; set; }
        public Organization Organization { get; set; }
        public HealthInsUser User { get; set; }
    }
}

using AutoMapper;
using HealthIns.Data.Models.Bussines.Enums;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.ViewModels.MoneyIn;
using HealthIns.Web.ViewModels.Premium;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Contract
{
    public class ContractViewModel : IMapFrom<ContractServiceModel>
    {
        public long Id { get; set; }
        public string ProductIdntfr { get; set; }
        public string PersonId { get; set; }
        public string DistributorId { get; set; }
        public string PersonFullName { get; set; }
        public string Frequency { get; set; }
        public Status Status { get; set; }
        public double Amount { get; set; }
        public double PremiumAmount { get; set; }
        public int Duration { get; set; }
        public DateTime NextBillingDueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<PremiumViewModel> PremiumsFound { get; set; }
        public List<MoneyInViewModel> MoneyInsFound { get; set; }
        public List<ClaimActivityViewModel> ClaimsFound { get; set; }
        public string SelectedTab { get; set; }


        public ContractViewModel()
        {
            this.SelectedTab = "summary";
            this.PremiumsFound = new List<PremiumViewModel>();
            this.MoneyInsFound = new List<MoneyInViewModel>();
            this.ClaimsFound = new List<ClaimActivityViewModel>();
        }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ContractServiceModel, ContractViewModel>();
            //.ForMember(destination => destination.PersonFullName,
            //            opts => opts.MapFrom(origin => origin.Person.FullName)); 

        }

    }
}


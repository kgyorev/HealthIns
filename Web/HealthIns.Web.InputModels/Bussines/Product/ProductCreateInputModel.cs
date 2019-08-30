using AutoMapper;
using HealthIns.Services.Mapping;
using HealthIns.Services.Models;
using HealthIns.Web.InputModels.Utils.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthIns.Web.InputModels.Bussines
{
   public class ProductCreateInputModel : IMapTo<ProductServiceModel>, IMapFrom<ProductServiceModel>
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Product Identifyer is required!")]
        [ProductIdntfrUniqeValidator]
        public string Idntfr { get; set; }

        [Required(ErrorMessage = "Label is required!")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Minimum Age is required!")]
        [Range(1, 100, ErrorMessage = "Value can be between 1 and 100!")]
        public int? MinAge { get; set; }

        [Required(ErrorMessage = "Maximum Age is required!")]
        [Range(1, 100, ErrorMessage = "Value can be between 1 and 100!")]
        public int? MaxAge { get; set; }

        [Required(ErrorMessage = "Frequency is required!")]
        public List<string> FrequencyRule { get; set; }
    }
}

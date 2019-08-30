using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Web.ViewModels.Common
{
    public abstract class SearchPagenationRoot
    {
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 2;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));


        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDesk.App.Models.ViewModels
{
    public class SolutionTablesViewModel
    {
        public IEnumerable<SolutionListingViewModel> MostViewed { get; set; }

        public IEnumerable<SolutionListingViewModel> MostRecent { get; set; }
    }
}

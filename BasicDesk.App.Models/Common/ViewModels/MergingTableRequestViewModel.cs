using System.Collections.Generic;
using X.PagedList;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class MergingTableRequestViewModel
    {
        public int Id { get; set; }

        public IPagedList<RequestMergeListingViewModel> Requests { get; set; }

        //public IEnumerable<RequestMergeListingViewModel> Requests { get; set; }
    }
}

using BasicDesk.Data.Models.Solution;
using System.Collections.Generic;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class SolutionDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedOn { get; set; }

        public string Author { get; set; }

        public IEnumerable<SolutionAttachment> Attachments { get; set; }
    }
}

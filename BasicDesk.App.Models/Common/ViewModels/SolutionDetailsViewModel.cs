using BasicDesk.Data.Models.Solution;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class SolutionDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedOn { get; set; }

        public string Author { get; set; }

        public SolutionAttachment Attachment { get; set; }
    }
}

using BasicDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Models.ViewModels
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

using BasicDesk.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class RequestReplyViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string CreationTime { get; set; }

        public IEnumerable<ReplyAttachment> Attachments { get; set; }
    }
}

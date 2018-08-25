﻿using BasicDesk.Models;

namespace BasicDesk.App.Models.ViewModels
{
    public class RequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string AssignedToName { get; set; }

        public string AssignedToEmail { get; set; }

        public string CreatedOn { get; set; }

        public string Status { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public RequestAttachment Attachment { get; set; }
    }
}

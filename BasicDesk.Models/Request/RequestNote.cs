using BasicDesk.Common.Constants.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class RequestNote
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestConstants.DescriptionMinLength)]
        [MaxLength(RequestConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public string Author { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}

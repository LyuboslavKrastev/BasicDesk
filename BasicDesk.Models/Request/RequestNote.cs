using BasicDesk.Common.Constants.Validation;
using BasicDesk.Data.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class RequestNote : IEntity
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

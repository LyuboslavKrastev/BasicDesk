using BasicDesk.Common.Constants.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class ApprovalCreationViewModel
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public string ApproverId { get; set; }

        [Required]
        public IEnumerable<SelectListItem> Users { get; set; }

        [Required]
        [MinLength(RequestConstants.SubjectMinLength)]
        [MaxLength(RequestConstants.SubjectMaxLength)]
        public string Subject { get; set; }

        [Required]
        [MinLength(RequestConstants.DescriptionMinLength)]
        [MaxLength(RequestConstants.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}

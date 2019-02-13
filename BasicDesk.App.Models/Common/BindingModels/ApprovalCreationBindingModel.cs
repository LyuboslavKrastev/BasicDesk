using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Models.Common.BindingModels
{
    public class ApprovalCreationBindingModel
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public string ApproverId { get; set; }

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

using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class ApprovalStatus
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestStatusConstants.NameMinLength)]
        [MaxLength(RequestStatusConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

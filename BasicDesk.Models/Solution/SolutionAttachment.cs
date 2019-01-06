using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Solution
{
    public class SolutionAttachment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(SolutionAttachmentConstants.FileNameMinLength)]
        [MaxLength(SolutionAttachmentConstants.FileNameMaxLength)]
        public string FileName { get; set; }

        [Required]
        [MinLength(SolutionAttachmentConstants.PathToFileMinLength)]
        [MaxLength(SolutionAttachmentConstants.PathToFileMaxLength)]
        public string PathToFile { get; set; }

        public int SolutionId { get; set; }
        public Solution Solution { get; set; }
    }
}

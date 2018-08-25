using BasicDesk.Common.Constants.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Models
{
    public class Solution
    {
        public int Id { get; set; }

        [Required]
        [MinLength(SolutionConstants.TitleMinLength)]
        [MaxLength(SolutionConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(SolutionConstants.ContentMinLength)]
        [MaxLength(SolutionConstants.ContentMaxLength)]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public ICollection<SolutionAttachment> Attachments { get; set; } = new List<SolutionAttachment>();
    }
}

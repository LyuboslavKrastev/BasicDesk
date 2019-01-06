using BasicDesk.Common.Constants.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Models.Management.BindingModels
{
    public class SolutionCreationBindingModel
    {
        [Required]
        [MinLength(SolutionConstants.TitleMinLength)]
        [MaxLength(SolutionConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(SolutionConstants.ContentMinLength)]
        [MaxLength(SolutionConstants.ContentMaxLength)]
        public string Content { get; set; }

        public IFormFile Attachment { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
}

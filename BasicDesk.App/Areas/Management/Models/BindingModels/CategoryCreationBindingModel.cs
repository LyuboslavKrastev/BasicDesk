using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Areas.Management.Models.BindingModels
{
    public class CategoryCreationBindingModel
    {
        [Required]
        [MinLength(RequestCategoryConstants.NameMinLength)]
        [MaxLength(RequestCategoryConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Models.Management.BindingModels
{
    public class CategoryEditingBindingModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestCategoryConstants.NameMinLength)]
        [MaxLength(RequestCategoryConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

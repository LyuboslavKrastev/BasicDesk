using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Areas.Management.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestCategoryConstants.NameMinLength)]
        [MaxLength(RequestCategoryConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

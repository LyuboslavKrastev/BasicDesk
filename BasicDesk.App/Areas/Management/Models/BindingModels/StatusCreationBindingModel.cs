using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;


namespace BasicDesk.App.Areas.Management.Models.BindingModels
{
    public class StatusCreationBindingModel
    {
        [Required]
        [MinLength(RequestStatusConstants.NameMinLength)]
        [MaxLength(RequestStatusConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

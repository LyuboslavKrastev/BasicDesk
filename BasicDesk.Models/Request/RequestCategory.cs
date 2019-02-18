using BasicDesk.Common.Constants.Validation;
using BasicDesk.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class RequestCategory : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestCategoryConstants.NameMinLength)]
        [MaxLength(RequestCategoryConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

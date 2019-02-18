using BasicDesk.Common.Constants.Validation;
using BasicDesk.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class RequestStatus : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestStatusConstants.NameMinLength)]
        [MaxLength(RequestStatusConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Models.Common.BindingModels
{
    public class UserLoggingInModel
    {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
    }
}

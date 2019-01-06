
using System.ComponentModel.DataAnnotations;


namespace BasicDesk.App.Models.Management.BindingModels
{
    public class RequestEditingBindingModel
    {
        [Display(Name = "Assign to")]
        public string AssignToId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}

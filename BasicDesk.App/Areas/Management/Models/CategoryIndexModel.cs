using BasicDesk.App.Areas.Management.Models.BindingModels;
using BasicDesk.App.Areas.Management.Models.ViewModels;
using System.Collections.Generic;

namespace BasicDesk.App.Areas.Management.Models
{
    public class CategoryIndexModel
    {
        public ICollection<CategoryViewModel> CategoryViewModels { get; set; }

        public CategoryCreationBindingModel CategoryCreationBindingModel { get; set; }
    }
}

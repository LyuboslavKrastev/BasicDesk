using BasicDesk.App.Models.Management.BindingModels;
using BasicDesk.App.Models.Management.ViewModels;
using System.Collections.Generic;

namespace BasicDesk.App.Models.Management
{
    public class CategoryIndexModel
    {
        public ICollection<CategoryViewModel> CategoryViewModels { get; set; }

        public CategoryCreationBindingModel CategoryCreationBindingModel { get; set; }
    }
}

using AutoMapper;
using BasicDesk.App.Areas.Management.Models;
using BasicDesk.App.Areas.Management.Models.BindingModels;
using BasicDesk.App.Areas.Management.Models.ViewModels;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Data;
using BasicDesk.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BasicDesk.App.Areas.Management.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly IMapper mapper;

        public CategoriesController(BasicDeskDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = this.dbContext.RequestCategories.ToArray();

            var model = new CategoryIndexModel
            {
                CategoryViewModels = mapper.Map<ICollection<CategoryViewModel>>(categories)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CategoryIndexModel model)
        {
            var category = this.mapper.Map<RequestCategory>(model.CategoryCreationBindingModel);
            if (dbContext.RequestCategories.Any(c => c.Name == category.Name))
            {
                this.TempData.Put("__Message", new MessageModel()
                {
                    Type = MessageType.Warning,
                    Message = "Category already exists"
                });

                return this.RedirectToAction("Index");
            }

            this.dbContext.RequestCategories.Add(category);
            this.dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Category created successfully"
            });

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var model = this.mapper.Map<CategoryViewModel>(this.dbContext.RequestCategories.Find(id));

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var requestCategory = dbContext.RequestCategories.Find(model.Id);
            requestCategory.Name = model.Name;

            this.dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Category updated successfully"
            });

            return this.Edit(model.Id);
        }
    }
}
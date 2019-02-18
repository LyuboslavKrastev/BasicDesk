using AutoMapper;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Data;
using BasicDesk.Data.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BasicDesk.App.Models.Management;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Services.Interfaces;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Management.BindingModels;
using BasicDesk.App.Common.Interfaces;
using System.Threading.Tasks;
using System;

namespace BasicDesk.App.Areas.Management.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoriesService service;
        private readonly IAlerter alerter;

        //private readonly BasicDeskDbContext dbContext;

        public CategoriesController(/*BasicDeskDbContext dbContext*/ICategoriesService service, IAlerter alerter)
        {
            this.service = service;
            this.alerter = alerter;
            //this.dbContext = dbContext;

        }

        public IActionResult Index()
        {
            var categories = this.service.GetAll();

            var model = new CategoryIndexModel
            {
                CategoryViewModels = categories.ProjectTo<CategoryViewModel>().ToArray()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CategoryIndexModel model)
        {
            var category = Mapper.Map<RequestCategory>(model.CategoryCreationBindingModel);
            //if (dbContext.RequestCategories.Any(c => c.Name == category.Name))
            //{
            //    this.TempData.Put("__Message", new MessageModel()
            //    {
            //        Type = MessageType.Warning,
            //        Message = "Category already exists"
            //    });

            //    return this.RedirectToAction("Index");
            //}

            Task added = this.service.AddAsync(category);
            if (added.IsFaulted)
            {
                return this.BadRequest();
            }

            Task success = this.service.SaveChangesAsync();
            if (success.IsFaulted)
            {
                return this.BadRequest();
            }

            alerter.AddMessage(MessageType.Success, "Category created successfully");

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var model = this.service.ById(id).ProjectTo<CategoryViewModel>().First();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this.service.Edit(model.Id, model.Name);

            alerter.AddMessage(MessageType.Success, "Category updated successfully");

            return this.RedirectToAction("Edit", new { id = model.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.Delete(new int[] { id });

            alerter.AddMessage(MessageType.Success, "Category updated successfully");

            return this.RedirectToAction("Index");
        }
    }
}
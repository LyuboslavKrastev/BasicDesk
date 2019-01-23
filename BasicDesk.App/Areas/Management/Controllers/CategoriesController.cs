﻿using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Data;
using BasicDesk.Data.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BasicDesk.App.Models.Management;
using BasicDesk.App.Models.Management.ViewModels;

namespace BasicDesk.App.Areas.Management.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        private readonly BasicDeskDbContext dbContext;

        public CategoriesController(BasicDeskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = this.dbContext.RequestCategories.ToArray();

            var model = new CategoryIndexModel
            {
                CategoryViewModels = Mapper.Map<ICollection<CategoryViewModel>>(categories)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CategoryIndexModel model)
        {
            var category = Mapper.Map<RequestCategory>(model.CategoryCreationBindingModel);
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
            var model = Mapper.Map<CategoryViewModel>(this.dbContext.RequestCategories.Find(id));

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
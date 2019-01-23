using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicDesk.App.Areas.Management.Pages.Reports
{
    public class ReportModel : BasePageModel
    {
        private class UserRequestsViewModel
        {
            public string Name { get; set; }
            public int RequestsCount { get; set; }
        }
        private readonly BasicDeskDbContext dbContext;

        public List<ReportViewModel> Model { get; set; }

        public ReportModel(BasicDeskDbContext dbContext)
        {
            this.Model = new List<ReportViewModel>();
            this.dbContext = dbContext;
        }

        public IActionResult OnGet()
        {

            var users = this.dbContext.Users.Select(u => new UserRequestsViewModel
            {
                Name = u.FullName,
                RequestsCount = u.Requests.Count()
            }).ToArray();

            foreach (var user in users)
            {
                Model.Add(new ReportViewModel
                {
                    DimensionOne = user.Name,
                    Quantity = user.RequestsCount
                });
            }

            TempData["Type"] = "bar";

            return this.Page();
        }   
    }
}
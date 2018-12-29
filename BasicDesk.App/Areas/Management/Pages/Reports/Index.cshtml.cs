using BasicDesk.App.Areas.Management.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BasicDesk.App.Areas.Management.Pages.Reports
{
    public class ReportModel : BasePageModel
    {

        public List<ReportViewModel> Model { get; set; }

        public ReportModel()
        {
            this.Model = new List<ReportViewModel>();
        }

        public IActionResult OnGet()
        {
            Random rnd = new Random();

            var rep = new ReportViewModel();
            rep.DimensionOne = "hi";
            rep.Quantity = 5;
            Model.Add(rep);
            Model.Add(new ReportViewModel {
                DimensionOne = "gr8 success",
                Quantity = 100
            });

            Model.Add(new ReportViewModel
            {
                DimensionOne = "gr8er success",
                Quantity = 150
            });

            Model.Add(new ReportViewModel
            {
                DimensionOne = "m@x success",
                Quantity = 200
            });

            TempData["Type"] = "bar";

            return this.Page();
        }   
    }
}
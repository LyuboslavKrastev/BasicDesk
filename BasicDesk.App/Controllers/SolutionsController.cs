using AutoMapper.QueryableExtensions;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Controllers
{
    [Authorize]
    public class SolutionsController :  ControllerWithAttachments<SolutionAttachment>
    {
        private readonly ISolutionService service;

        public SolutionsController(ISolutionService service, AttachmentService<SolutionAttachment> attachmentService) : base(attachmentService)
        {
            this.service = service;
        }

        public IActionResult Index()
        {      
            var solutions = service.GetAll().ProjectTo<SolutionListingViewModel>().ToArray();

            SolutionTablesViewModel model = new SolutionTablesViewModel
            {
                MostRecent = solutions.OrderByDescending(s => s.CreationTime),

                MostViewed = solutions.OrderByDescending(s => s.Views)
            };


            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {

            var solution = await this.service.GetSolutionDetails(id);

            if (solution == null)
            {
                return NotFound();
            }

            return this.View(solution);
        }
    }
}
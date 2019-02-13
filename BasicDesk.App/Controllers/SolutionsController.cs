using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Controllers
{
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly ISolutionService service;

        public SolutionsController(BasicDeskDbContext dbContext, ISolutionService service)
        {
            this.service = service;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {      
            var solutions = service.GetAll();

            SolutionTablesViewModel model = new SolutionTablesViewModel();

            model.MostRecent = solutions.OrderByDescending(s => s.CreationTime);

            model.MostViewed = solutions.OrderByDescending(s => s.Views);


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

        public async Task<IActionResult> Download(string fileName, string filePath, string solutionId)
        {
            try
            {
                if (fileName == null)
                    return Content("filename not present");

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
            }
            catch (IOException)
            {
                this.TempData.Put("__Message", new MessageModel()
                {
                    Type = MessageType.Warning,
                    Message = "File still uploading"
                });

                return this.RedirectToAction("Details", new { id = solutionId });
            }
        }

        private string GetContentType(string path)
        {
            var types = FileFormatValidator.GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
    }
}
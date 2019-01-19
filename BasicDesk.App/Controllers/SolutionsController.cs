using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BasicDesk.App.Controllers
{
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly SolutionService service;

        public SolutionsController(BasicDeskDbContext dbContext, SolutionService service)
        {
            this.service = service;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {      
            var solutionListingModels = service.GetAll();

            return View(solutionListingModels);
        }

        public IActionResult Details(int id)
        {
            var solution = this.service.GetSolutionDetails(id);

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
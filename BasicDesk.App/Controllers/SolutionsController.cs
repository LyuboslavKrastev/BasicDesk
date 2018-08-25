using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Models;
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
        private readonly IMapper mapper;
        private readonly BasicDeskDbContext dbContext;

        public SolutionsController(BasicDeskDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {      
            var solutionListingModels = await GetAllSolutionsAsync();

            return View(solutionListingModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var solution = await GetSolutionDetails(id);

            if (solution == null)
            {
                return BadRequest();
            }

            var model = mapper.Map<SolutionDetailsViewModel>(solution);

            return this.View(model);
        }

        private async Task<Solution> GetSolutionDetails(int id)
        {
            return await this.dbContext.Solutions
                .Include(s => s.Author)
                .Include(s => s.Attachments)
                .FirstOrDefaultAsync(s => s.Id == id);
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

        private async Task<ICollection<SolutionListingViewModel>> GetAllSolutionsAsync()
        {
            var solutions = await this.dbContext.Solutions.Include(s => s.Author).ToArrayAsync();

            return this.mapper.Map<ICollection<SolutionListingViewModel>>(solutions);
        }
        
    }
}
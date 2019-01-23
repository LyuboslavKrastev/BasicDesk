using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Data;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BasicDesk.App.Models.Management.BindingModels;

namespace BasicDesk.App.Areas.Management.Pages.Solutions
{
    public class CreateModel : BasePageModel
    {
        private BasicDeskDbContext dbContext;
        private UserManager<User> userManager;

        [BindProperty]
        public SolutionCreationBindingModel Model {get; set; }

        public CreateModel(BasicDeskDbContext dbContext, UserManager<User> userManager)
        {
            this.Model = new SolutionCreationBindingModel();
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var solution = Mapper.Map<Solution>(Model);
            solution.AuthorId = this.userManager.GetUserId(User);

            this.dbContext.Solutions.Add(solution);

            if (Model.Attachment != null)
            {
                var extension = Model.Attachment.FileName.Split('.').Last();

                var isAllowedFileFormat = FileFormatValidator.IsValidFormat(extension);

                if (!isAllowedFileFormat)
                {
                    this.TempData.Put("__Message", new MessageModel()
                    {
                        Type = MessageType.Danger,
                        Message = "Forbidden file type!"
                    });
                    return this.Page();
                }

                await CreateAttachmentAsync(Model, solution);
            }

            this.dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Solution created successfully"
            });


            return RedirectToPage("/Index");
        }

        private async Task CreateAttachmentAsync(SolutionCreationBindingModel model, Solution solution)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Solutions", model.Attachment.FileName);
            var fileStream = new FileStream(path, FileMode.Create);
            using (fileStream)
            {
                await model.Attachment.CopyToAsync(fileStream);
            }

            this.dbContext.SolutionAttachments.Add(new SolutionAttachment
            {
                FileName = model.Attachment.FileName,
                PathToFile = path,
                SolutionId = solution.Id
            });
        }
    }
}
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
using BasicDesk.Services;
using BasicDesk.Services.Interfaces;
using System.Collections.Generic;
using BasicDesk.Data.Models.Requests;
using BasicDesk.App.Common.Interfaces;

namespace BasicDesk.App.Areas.Management.Pages.Solutions
{
    public class CreateModel : BasePageModel
    {
        private readonly ISolutionService solutionService;
        private UserManager<User> userManager;
        private readonly AttachmentService<SolutionAttachment> attachmentService;
        private readonly IFileUploader fileUploader;
        private readonly IAlerter alerter;

        [BindProperty]
        public SolutionCreationBindingModel Model {get; set; }

        public CreateModel(ISolutionService solutionService, UserManager<User> userManager, 
            AttachmentService<SolutionAttachment>  attachmentService, IFileUploader fileUploader, IAlerter alerter)
        {
            this.Model = new SolutionCreationBindingModel();
            this.solutionService = solutionService;
            this.userManager = userManager;
            this.attachmentService = attachmentService;
            this.fileUploader = fileUploader;
            this.alerter = alerter;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var solution = Mapper.Map<Solution>(Model);
            solution.AuthorId = this.userManager.GetUserId(User);


            await this.solutionService.AddAsync(solution);

            if (Model.Attachments != null)
            {
                string path = await fileUploader.CreateAttachmentAsync(Model.Title, Model.Attachments, "Solutions");

                ICollection<SolutionAttachment> attachments = new List<SolutionAttachment>();

                foreach (var attachment in Model.Attachments)
                {
                    SolutionAttachment solutionAttachment = new SolutionAttachment
                    {
                        FileName = attachment.FileName,
                        PathToFile = Path.Combine(path, attachment.FileName),
                        SolutionId = solution.Id
                    };
                    attachments.Add(solutionAttachment);
                }

                await this.attachmentService.AddRangeAsync(attachments);
            }

            await  this.solutionService.SaveChangesAsync();

            alerter.AddMessage(MessageType.Success, "Solution created successfully");

            return Redirect($"/Solutions/Details/{solution.Id}");
        }
    }
}
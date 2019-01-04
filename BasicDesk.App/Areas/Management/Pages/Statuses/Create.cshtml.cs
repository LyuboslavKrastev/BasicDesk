using BasicDesk.App.Areas.Management.Pages;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Common.Constants;
using BasicDesk.Common.Constants.Validation;
using BasicDesk.Data;
using BasicDesk.Models;
using BasicDesk.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BasicDesk.App.Areas.Management.Pages.Statuses
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class CreateModel : PageModel
    {
        private BasicDeskDbContext dbContext;

        [BindProperty]
        [Required]
        [MinLength(RequestStatusConstants.NameMinLength)]
        [MaxLength(RequestStatusConstants.NameMaxLength)]
        public string StatusName {get; set; }

        public CreateModel(BasicDeskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var status = new RequestStatus
            {
                Name = StatusName
            };

            await this.dbContext.RequestStatuses.AddAsync(status);

            this.dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Status created successfully"
            });


            return RedirectToPage("/Index");
        }     
    }
}
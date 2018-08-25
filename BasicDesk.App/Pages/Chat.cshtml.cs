using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BasicDesk.Data;
using BasicDesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicDesk.App.Pages
{
    [Authorize]
    public class ChatModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly BasicDeskDbContext dbContext;

        public ChatModel(UserManager<User> userManager, BasicDeskDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        
        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userId = this.userManager.GetUserId(User);

            var user = await this.dbContext.Users.FindAsync(userId);

            this.FullName = user.FullName;

            return this.Page();
        }
    }
}
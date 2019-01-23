using AutoMapper;
using BasicDesk.App.Areas.Management.Pages;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data;

namespace BasicDesk.App.Pages
{
    public class DetailsModel : BasePageModel
    {
        private readonly BasicDeskDbContext dbContext;

        public DetailsModel(BasicDeskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserDetailsViewModel UserDetailsView { get; set; }

        public void OnGet(string id)
        {
            var user = this.dbContext.Users.Find(id);

            this.UserDetailsView = Mapper.Map<UserDetailsViewModel>(user);
        }
    }
}
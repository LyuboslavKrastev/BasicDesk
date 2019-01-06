using AutoMapper;
using BasicDesk.App.Areas.Management.Pages;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data;

namespace BasicDesk.App.Pages
{
    public class DetailsModel : BasePageModel
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly IMapper mapper;

        public DetailsModel(BasicDeskDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public UserDetailsViewModel UserDetailsView { get; set; }

        public void OnGet(string id)
        {
            var user = this.dbContext.Users.Find(id);

            this.UserDetailsView = this.mapper.Map<UserDetailsViewModel>(user);
        }
    }
}
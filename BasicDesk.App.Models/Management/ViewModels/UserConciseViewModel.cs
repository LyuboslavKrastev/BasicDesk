namespace BasicDesk.App.Models.Management.ViewModels
{
    public class UserConciseViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsHelpdeskAgent { get; set; }

        public bool IsBanned { get; set; }
    }
}

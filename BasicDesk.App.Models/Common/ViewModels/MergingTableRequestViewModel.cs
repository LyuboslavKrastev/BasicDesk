namespace BasicDesk.App.Models.Common.ViewModels
{
    public class MergingTableRequestViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Author { get; set; }

        public string CreationTime { get; set; }

        public string AssignedTo { get; set; }
    }
}

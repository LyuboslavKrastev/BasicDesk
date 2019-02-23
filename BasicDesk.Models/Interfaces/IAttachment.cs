using BasicDesk.Data.Models.Interfaces;

namespace BasicDesk.Data.Models.Requests
{
    public interface IAttachment : IEntity
    {
        string FileName { get; set; }
        string PathToFile { get; set; }
    }
}
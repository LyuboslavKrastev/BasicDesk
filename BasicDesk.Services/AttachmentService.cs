using BasicDesk.Data.Models.Interfaces;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Repository;

namespace BasicDesk.Services
{
    public class AttachmentService<T> : BaseDbService<T>
        where T: class, IEntity, IAttachment 
    {
        public AttachmentService(IRepository<T> repository) : base(repository)
        {
        }


    }
}

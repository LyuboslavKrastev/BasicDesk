using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;

namespace BasicDesk.Services
{
    public class StatusService : BaseDbService<RequestStatus>, IDbService<RequestStatus>
    {
        public StatusService(IRepository<RequestStatus> repository) : base(repository)
        {
        }
    }
}

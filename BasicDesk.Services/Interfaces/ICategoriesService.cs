using System.Linq;
using System.Threading.Tasks;
using BasicDesk.Data.Models.Requests;

namespace BasicDesk.Services.Interfaces
{
    public interface ICategoriesService : IDbService<RequestCategory>
    {
        Task Edit(int id, string name);
    }
}
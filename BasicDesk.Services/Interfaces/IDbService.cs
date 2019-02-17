using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services.Interfaces
{
    public interface IDbService<T>
    {
        IQueryable<T> ById(int id);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        Task Delete(IEnumerable<int> ids);
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();
        TEntity ById(int id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Delete(IEnumerable<TEntity> entity);

        Task<int> SaveChangesAsync();
    }
}

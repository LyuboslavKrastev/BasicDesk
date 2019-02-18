using BasicDesk.Data.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services.Repository
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> ById(int id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Delete(IEnumerable<int> entities);

        Task<int> SaveChangesAsync();
    }
}

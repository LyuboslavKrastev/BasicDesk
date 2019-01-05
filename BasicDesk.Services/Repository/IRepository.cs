using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services.Repository
{
    interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task<TEntity> ById(int id);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}

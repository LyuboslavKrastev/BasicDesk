using BasicDesk.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services.Repository
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly BasicDeskDbContext dbContext;
        private DbSet<TEntity> dbSet;

        public DbRepository(BasicDeskDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }
        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return this.dbSet.AddRangeAsync(entities);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public TEntity ById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            this.dbSet.RemoveRange(entities);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }
    }
}

using BasicDesk.Data;
using BasicDesk.Data.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services.Repository
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class, IEntity
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

        public IQueryable<TEntity> ById(int id)
        {
            return this.dbSet.Where(e => e.Id == id);
        }

        public void Delete(IEnumerable<int> ids)
        {
            IEnumerable<TEntity> entities = this.All().Where(r => ids.Contains(r.Id));
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

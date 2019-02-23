﻿using BasicDesk.Data.Models.Interfaces;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public abstract class BaseDbService<T> : IDbService<T> where T : class, IEntity
    {
        protected readonly IRepository<T> repository;

        protected BaseDbService(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public Task AddAsync(T entity)
        {
            return this.repository.AddAsync(entity);
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return this.repository.AddRangeAsync(entities);
        }

        public IQueryable<T> ById(int id)
        {
            return this.repository.ById(id);
        }

        public IQueryable<T> GetAll()
        {
            return this.repository.All();
        }

        public async Task DeleteRange(IEnumerable<int> ids)
        {
            this.repository.DeleteRange(ids);

            await this.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            this.repository.Delete(id);

            await this.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return this.repository.SaveChangesAsync();
        }

 
    }
}

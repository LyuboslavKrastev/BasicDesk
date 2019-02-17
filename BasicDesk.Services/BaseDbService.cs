//using BasicDesk.Services.Interfaces;
//using BasicDesk.Services.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BasicDesk.Services
//{
//    public abstract class BaseDbService<T> : IDbService<T> where T : class
//    {
//        private readonly IRepository<T> repository;

//        protected BaseDbService(IRepository<T> repository)
//        {
//            this.repository = repository;
//        }
//        public Task AddAsync(T entity)
//        {
//            return this.repository.AddAsync(entity);
//        }

//        public T ById(int id)
//        {
//            return this.repository.ById(id);
//        }

//        public async Task Delete(IEnumerable<T> entities)
//        {
//            this.repository.Delete(entities);

//            await this.SaveChangesAsync();
//        }

//        public Task Delete(IEnumerable<int> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public IQueryable<T> GetAll()
//        {
//            return this.repository.All();
//        }

//        public Task SaveChangesAsync()
//        {
//            return this.repository.SaveChangesAsync();
//        }
//    }
//}

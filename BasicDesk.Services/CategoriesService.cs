using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<RequestCategory> repository;

        public CategoriesService(IRepository<RequestCategory> repository)
        {
            this.repository = repository;
        }
        public Task AddAsync(RequestCategory category)
        {
            return this.repository.AddAsync(category);
        }

        public IQueryable<RequestCategory> GetAll()
        {
            return this.repository.All();
        }

        public IQueryable<RequestCategory> ById(int id)
        {
            return this.repository.All().Where(c => c.Id == id);
        }

        public async Task Edit(int id, string name)
        {
            var category = await this.repository.All().FirstAsync(c => c.Id == id);

            category.Name = name;
        }

        public Task SaveChangesAsync()
        {
            return this.repository.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<int> ids)
        {
            IQueryable<RequestCategory> categories = this.repository.All()
              .Where(c => ids.Contains(c.Id));
            this.repository.Delete(categories);

            await this.SaveChangesAsync();
        }
    }
}

using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class CategoriesService : BaseDbService<RequestCategory>, ICategoriesService, IDbService<RequestCategory>
    {
        public CategoriesService(IRepository<RequestCategory> repository) : base(repository)
        {
        }

        public async Task Edit(int id, string name)
        {
            var category = await this.repository.All().FirstAsync(c => c.Id == id);

            category.Name = name;

            await this.SaveChangesAsync();
        }
    }
}

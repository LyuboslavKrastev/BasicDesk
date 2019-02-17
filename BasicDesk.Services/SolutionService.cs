using AutoMapper;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class SolutionService : ISolutionService, IDbService<Solution>
    {
        private readonly DbRepository<Solution> repository;

        public SolutionService(DbRepository<Solution> repository)
        {
            this.repository = repository;
        }

        public Task AddAsync(Solution solution)
        {
            return this.repository.AddAsync(solution);
        }
        public Task SaveChangesAsync()
        {
            return this.repository.SaveChangesAsync();
        }

        public async Task<SolutionDetailsViewModel> GetSolutionDetails(int id)
        {
            Solution solution = await this.ById(id)
                .Include(s => s.Author)
                .Include(s => s.Attachments)
                .FirstAsync(s => s.Id == id);
            solution.Views++;
            await this.SaveChangesAsync();
            return Mapper.Map<SolutionDetailsViewModel>(solution);
        }

        public IQueryable<Solution> ById(int id)
        {
            return this.repository.All().Where(s => s.Id == id);
        }

        public IQueryable<Solution> GetAll()
        {
            return this.repository.All();
        }

        public Task Delete(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class SolutionService : ISolutionService
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

        public IEnumerable<SolutionListingViewModel> GetAll()
        {
            return this.repository.All().ProjectTo<SolutionListingViewModel>();
        }    


        public async Task<SolutionDetailsViewModel> GetSolutionDetails(int id)
        {
            Solution solution = await this.repository.All()
                .Include(s => s.Author)
                .Include(s => s.Attachments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solution == null)
            {
                return null;
            }
            solution.Views++;
            await this.SaveChangesAsync();
            return Mapper.Map<SolutionDetailsViewModel>(solution);
        }
    }
}

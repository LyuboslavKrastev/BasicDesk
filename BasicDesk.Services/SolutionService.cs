using AutoMapper;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services.AutoMapping;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class SolutionService
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


        public SolutionDetailsViewModel GetSolutionDetails(int id)
        {
            return this.repository.All().Where(s => s.Id == id).ProjectTo<SolutionDetailsViewModel>().FirstOrDefault();
        }
    }
}

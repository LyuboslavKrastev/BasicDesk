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
    //CHANGED TEST THIS
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

        //public async Task<IActionResult> Details(int id)
        //{
        //    var solution = await GetSolutionDetails(id);

        //    if (solution == null)
        //    {
        //        return BadRequest();
        //    }

        //    var model = mapper.Map<SolutionDetailsViewModel>(solution);

        //    return this.View(model);
        //}

        public SolutionDetailsViewModel GetSolutionDetails(int id)
        {
            return this.repository.All().Where(s => s.Id == id).ProjectTo<SolutionDetailsViewModel>().FirstOrDefault();
        }

        //  private async Task<ICollection<SolutionListingViewModel>> GetAllSolutionsAsync()
        // {
        //     var solutions = await this.dbContext.Solutions.ToArrayAsync();

        //     return this.mapper.Map<ICollection<SolutionListingViewModel>>(solutions);
        // }
    }
}

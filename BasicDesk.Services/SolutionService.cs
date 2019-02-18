using AutoMapper;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class SolutionService : BaseDbService<Solution>, ISolutionService, IDbService<Solution>
    {


        public SolutionService(DbRepository<Solution> repository) : base(repository)
        {
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
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data.Models.Solution;

namespace BasicDesk.Services.Interfaces
{
    public interface ISolutionService
    {
        Task AddAsync(Solution solution);
        IEnumerable<SolutionListingViewModel> GetAll();
        Task<SolutionDetailsViewModel> GetSolutionDetails(int id);
        Task SaveChangesAsync();
    }
}
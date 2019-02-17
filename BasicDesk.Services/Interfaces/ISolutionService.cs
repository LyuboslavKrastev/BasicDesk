using System.Threading.Tasks;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Data.Models.Solution;

namespace BasicDesk.Services.Interfaces
{
    public interface ISolutionService : IDbService<Solution>
    {
        Task<SolutionDetailsViewModel> GetSolutionDetails(int id);
    }
}
using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface ITeamRepository
    {
        Task<TeamResponseModel> AddTeamAsync(Team team);
        Task<TeamListResponseModel> GetAllTeamsAsync();
        Task<TeamResponseModel> GetTeamByIdAsync(Guid teamId);
        Task<TeamResponseModel> UpdateTeamAsync(Team team);
    }
}

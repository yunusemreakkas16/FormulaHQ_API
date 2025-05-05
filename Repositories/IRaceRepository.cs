using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IRaceRepository
    {
        Task<RaceResponseModel> AddRaceAsync(Race race);
        Task<RaceResponseListModel> GetAllRaceAsync();
        Task<RaceResponseModel> GetRaceByIdAsync(Guid driverId);
        Task<RaceResponseModel> UpdateRaceAsync(Race race);
    }
}

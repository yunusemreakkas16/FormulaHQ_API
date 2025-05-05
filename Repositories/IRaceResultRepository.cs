using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IRaceResultRepository
    {
        Task<RaceResultListResponseModel> GetAllRaceResultsAsync();
        Task<RaceResultResponseModel> GetRaceResultbyIDAsync(Guid raceResultID);
        Task<RaceResultResponseModel> CreateRaceResultAsync(RaceResult raceResult);
        Task<RaceResultResponseModel> UpdateRaceResultAsync(RaceResult raceResult);

    } 
}

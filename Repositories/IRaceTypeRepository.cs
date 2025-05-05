using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IRaceTypeRepository
    {
        Task<RacetypeListResponseModel> GetAllRaceTypesAsync();
        Task<RaceTypeResponseModel> GetRaceTypeByIdAsync(Guid typeId);
        Task<RaceTypeResponseModel> CreateRaceTypeAsync(RaceType raceType);
        Task<RaceTypeResponseModel> UpdateRaceTypeAsync(RaceType raceType);
    }
}

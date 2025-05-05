using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface ISeasonRepository
    {
        Task<SeasonResponseModel> AddSeasonAsync(Season season);
        Task<SeasonListResponseModel> GetAllSeasonsAsync();
        Task<SeasonResponseModel> GetSeasonByIdAsync(Guid seasonId);
        Task<SeasonResponseModel> UpdateSeasonAsync(Season season);
        Task<SeasonResponseModel> DeleteSeasonAsync(Guid seasonId);
    }
}

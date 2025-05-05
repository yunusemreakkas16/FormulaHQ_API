using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IFIARegulationRepository
    {
        Task<FIARegulationResponseModel> GetFIARegulationByIDAsync(FIARegulationParamModel regulationParam);
        Task<FIARegulationListResponseModel> GetAllFIARegulationsAsync();
        Task<FIARegulationResponseModel> AddFIARegulationAsync(FIARegulation regulation);
        Task<FIARegulationResponseModel> UpdateFIARegulationAsync(FIARegulation regulation);
        Task<FIARegulationResponseModel> DeleteFIARegulationAsync(Guid FIARegulationID);
    }
}

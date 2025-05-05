using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface ICircuitRepository
    {
        Task<CircuitResponseModel> GetCircuitByIdAsync(Guid circuitID);
        Task<CircuitListResponseModel> GetAllCircuitsAsync();
        Task<CircuitResponseModel> AddCircuitAsync(Circuit circuit);
        Task<CircuitResponseModel> UpdateCircuitAsync(Circuit circuit);
    }
}

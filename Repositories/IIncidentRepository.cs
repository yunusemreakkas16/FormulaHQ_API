using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IIncidentRepository
    {
        Task<IncidentResponseModel> GetIncidentByIDAsync(IncidentParamModel incidentParamModel);
        Task<IncidentListResponseModel> GetAllIncidentsAsync();
        Task<IncidentResponseModel> AddIncidentAsync(Incident incident);
        Task<IncidentResponseModel> UpdateIncidentAsync(Incident incident);

    }
}

using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IDriverRepository
    {
        Task<DriverResponseModel> AddDriverAsync(Driver driver);
        Task<DriverListResponseModel> GetAllDriversAsync();
        Task<DriverResponseModel> GetDriverByIdAsync(Guid driverId);
        Task<DriverResponseModel> UpdateDriverAsync(Driver driver);
    }
}

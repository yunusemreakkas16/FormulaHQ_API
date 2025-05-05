using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface ICarRepository
    {
        Task<CarResponseModel> GetCarByIDAsync(Guid carID);
        Task<CarListResponseModel> GetAllCarsAsync();
        Task<CarResponseModel> AddCarAsync(Car car);
        Task<CarResponseModel> UpdateCarAsync(Car car);
    }
}

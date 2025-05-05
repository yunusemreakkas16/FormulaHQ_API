using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface IUserRepository
    {
        Task<UserResponseModel> GetUserByIdAsync(Guid userId);
        Task<UserListResponseModel> GetAllUsersAsync();
        Task<UserResponseModel> CreateUserAsync(User user);
        Task<UserResponseModel> UpdateUserAsync(User user);
    }
}

using SERVPRO.API.User.Models;

namespace SERVPRO.API.User.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(int id);
    }
}

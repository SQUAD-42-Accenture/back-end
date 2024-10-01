using SERVPRO.API.Models;

namespace SERVPRO.API.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> SearchAllUser();
        Task<UserModel> SearchForCpf(string cpf);
        Task<UserModel> Add(UserModel user);
        Task<UserModel> Update(UserModel user, string cpf);
        Task<bool> Delete(string cpf);
    }
}

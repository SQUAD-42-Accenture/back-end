using Microsoft.EntityFrameworkCore;
using SERVPRO.API.Data;
using SERVPRO.API.Models;
using SERVPRO.API.Repository.Interface;

namespace SERVPRO.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext applicationDBContext) 
        {
            _dbContext = applicationDBContext;
        }
        public async Task<List<UserModel>> SearchAllUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> SearchForCpf(string cpf)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Cpf == cpf);
        }
        public async Task<UserModel> Add(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
             await _dbContext.SaveChangesAsync();

            return user;
        }
        public async Task<UserModel> Update(UserModel user, string cpf)
        {
            UserModel userForCpf = await SearchForCpf(cpf);
            if (userForCpf == null)
            {
                throw new Exception($"Usuário com CPF: {cpf} não foi encontrado.");
            }
            userForCpf.Name = user.Name;
            userForCpf.Name = user.Email;
            userForCpf.Name = user.Cpf;
            _dbContext.Users.Update(userForCpf);
            await _dbContext.SaveChangesAsync();

            return userForCpf;
        }

        public async Task<bool> Delete(string cpf)
        {
            UserModel userForCpf = await SearchForCpf(cpf);
            if (userForCpf == null)
            {
                throw new Exception($"Usuário com CPF: {cpf} não foi encontrado.");
            }
            _dbContext.Users.Remove(userForCpf);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

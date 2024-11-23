using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

    namespace SERVPRO.Repositorios
{
    public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private readonly ServproDBContext _dbContext;
        private object _dbcontext;

        public UsuarioRepositorio(ServproDBContext servproDBContext)
        {
            _dbContext = servproDBContext;
        }
        // Método para buscar usuário por CPF
        public async Task<Usuario> BuscarPorCpf(string cpf)
        {
            return await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.CPF == cpf);
        }
        public async Task<List<Usuario>> BuscarPorTipoUsuario(string TipoUsuario)
        {
            return await _dbContext.Usuarios
                .Where(u => u.TipoUsuario == TipoUsuario) // Aplica o filtro pelo TipoUsuario
                .ToListAsync(); // Converte o resultado em uma lista
        }
        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios
                .ToListAsync();
        }


    }
}

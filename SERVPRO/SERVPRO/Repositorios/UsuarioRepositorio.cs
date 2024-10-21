using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

    namespace SERVPRO.Repositorios
{
    public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public async Task<Usuario> BuscarPorTipoUsuario(string TipoUsuario)
        {
            return await _dbContext.Usuarios
                .Include(x => x.TipoUsuario)
                .FirstOrDefaultAsync(x => x.TipoUsuario == TipoUsuario);
        }

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios
                .Include(x => x.Nome)
                .ToListAsync();
        }
  

    }
}

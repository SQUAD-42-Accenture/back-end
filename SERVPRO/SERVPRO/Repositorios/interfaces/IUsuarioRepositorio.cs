using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<Usuario>> BuscarTodosUsuarios();
        Task<Usuario> BuscarPorTipoUsuario(String TipoUsuario);
     
    }
}

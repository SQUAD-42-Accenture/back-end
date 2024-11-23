using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IUsuarioRepositorio
    {
        Task <List<Usuario>> BuscarTodosUsuarios();
        Task<List<Usuario>> BuscarPorTipoUsuario(String TipoUsuario);
        Task<Usuario> BuscarPorCpf(string cpf);  

    }
}

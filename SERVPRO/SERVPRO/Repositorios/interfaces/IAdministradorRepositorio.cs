using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IAdministradorRepositorio
    {
        Task<List<Administrador>> BuscarTodosAdministradores();
        Task<Administrador> BuscarPorCPF(string cpf);
        Task<Administrador> Adicionar(Administrador administrador);
        Task<Administrador> Atualizar(Administrador administrador, string cpf);
        Task<bool> Apagar(string cpf);
    }
}

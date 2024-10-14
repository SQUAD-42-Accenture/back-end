using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> BuscarTodosClientes();
        Task<Cliente> BuscarPorCPF(String CPF);
        Task<Cliente> Adicionar(Cliente cliente);
        Task<Cliente> Atualizar(Cliente cliente, string cpf);
        Task<bool> Apagar(string cpf);
    }   
}

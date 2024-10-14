using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface ITecnicoRepositorio
    {
        Task<List<Tecnico>> BuscarTodosTecnicos();
        Task<Tecnico> BuscarPorCPF(String CPF);
        Task<Tecnico> Adicionar(Tecnico tecnico);
        Task<Tecnico> Atualizar(Tecnico tecnico, string cpf);
        Task<bool> Apagar(string cpf);
    }   
}

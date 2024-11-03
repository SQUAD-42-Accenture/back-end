using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IServicoRepositorio
    {
        Task<List<Servico>> BuscarTodosServicos();
        Task<Servico> BuscarPorId(int id);
        Task<Servico> Adicionar(Servico servico);
        Task<Servico> Atualizar(Servico servico, int id);
        Task<bool> Apagar(int id);
    }
}

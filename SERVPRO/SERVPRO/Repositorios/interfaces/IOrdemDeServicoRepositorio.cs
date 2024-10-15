using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IOrdemDeServicoRepositorio
    {
        Task<List<OrdemDeServico>> BuscarTodasOS();
        Task<OrdemDeServico> BuscarPorId(int id);
        Task<OrdemDeServico> Adicionar(OrdemDeServico ordemDeServico);
        Task<OrdemDeServico> Atualizar(OrdemDeServico ordemDeServico, int id);
        Task<bool> Apagar(int id);
    }
}

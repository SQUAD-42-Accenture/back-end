using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IHistoricoOsRepositorio
    {
        Task<List<HistoricoOS>> BuscarTodoshistoricos();
        Task<HistoricoOS> BuscarPorId(int id);
        Task<HistoricoOS> Adicionar(HistoricoOS historicoOS);
        Task<HistoricoOS> Atualizar(HistoricoOS historicoOS, int id);
        Task<bool> Apagar(int id);
    }
}

using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IEquipamentoRepositorio
    {
        Task<List<Equipamento>> BuscarTodosEquipamentos();
        Task<Equipamento> BuscarPorSerial(String serial);
        Task<Equipamento> Adicionar(Equipamento equipamento);
        Task<Equipamento> Atualizar(Equipamento equipamento, string serial);
        Task<bool> Apagar(string serial);
    }   
}

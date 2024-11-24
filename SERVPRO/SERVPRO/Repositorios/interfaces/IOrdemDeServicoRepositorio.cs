using SERVPRO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVPRO.Repositorios.Interfaces
{
    public interface IOrdemDeServicoRepositorio
    {
        Task<List<OrdemDeServico>> BuscarTodasOS();
        Task<OrdemDeServico> BuscarPorId(int id);
        Task<OrdemDeServico> Adicionar(OrdemDeServico ordemDeServico);
        Task<OrdemDeServico> Atualizar(int id, OrdemDeServico ordemDeServico);
        Task<List<OrdemDeServico>> BuscarOrdensPorCpfTecnico(string cpf);
        Task<List<OrdemDeServico>> BuscarOrdensPorCpfCliente(string cpf);
        Task<bool> Apagar(int id);

        Task<decimal> CalcularValorTotal(int ordemDeServicoId);
    }
}

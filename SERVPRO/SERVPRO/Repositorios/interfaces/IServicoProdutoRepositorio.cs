using SERVPRO.Models;

namespace SERVPRO.Repositorios.Interfaces
{
    public interface IServicoProdutoRepositorio
    {

        Task<List<ServicoProduto>> BuscarTodosServicoProdutos();


        Task<ServicoProduto> BuscarPorId(int id);

        Task<ServicoProduto> BuscarPorServicoProduto(int servicoId, int produtoId);


        Task<ServicoProduto> Adicionar(ServicoProduto servicoProduto);

        Task<ServicoProduto> AtualizarCustoProdutoNoServico(int servicoId, int produtoId, decimal novoCusto);


        Task<bool> Remover(int servicoId, int produtoId);

  
        Task<List<Produto>> ObterProdutosPorServico(int servicoId);

       
        Task<List<Servico>> ObterServicosPorProduto(int produtoId);
    }
}

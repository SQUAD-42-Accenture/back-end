using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<Produto>> BuscarTodosProdutos();
        Task<Produto> BuscarPorId(String IdProduto);
        Task<Produto> BuscarPorNome(String NomeProduto);
        Task<Produto> Adicionar(Equipamento produto);
        Task<Produto> Atualizar(Equipamento produto, string IdProduto);
        Task<bool> Apagar(string IdProduto);
    }
}

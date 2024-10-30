using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<Produto>> BuscarTodosProdutos();
        Task<Produto> BuscarPorId(String IdProduto);
        Task<List<Produto>> BuscarPorNome(string NomeProduto);
        Task<Produto> Adicionar(Produto produto);
        Task<Produto> Atualizar(Produto produto, string IdProduto);
        Task<bool> Apagar(string IdProduto);
    }
}

using SERVPRO.Models;

namespace SERVPRO.Repositorios.interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<Produto>> BuscarTodosProdutos();
        Task<Produto> BuscarPorId(int IdProduto);
        Task<List<Produto>> BuscarPorNome(string NomeProduto);
        Task<Produto> Adicionar(Produto produto);
        Task<Produto> Atualizar(Produto produto, int IdProduto);
        Task<bool> Apagar(int IdProduto);
    }
}

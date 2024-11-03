using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly ServproDBContext _dbContext;
        public ProdutoRepositorio(ServproDBContext servproDBContext)
        {
            _dbContext = servproDBContext;
        }
        public async Task<Produto> BuscarPorId(int IdProduto)
        {
            return await _dbContext.Produtos
     
                .FirstOrDefaultAsync(x => x.IdProduto == IdProduto);
        }

        public async Task<List<Produto>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos
                .ToListAsync();
        }

        public async Task<List<Produto>> BuscarPorNome(string NomeProduto)
        {
            return await _dbContext.Produtos
                .Where(x => x.NomeProduto == NomeProduto)
                .ToListAsync();
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            return produto;
        }
        public async Task<Produto> Atualizar(Produto produto, int IdProduto)
        {
            Produto produtoPorId = await BuscarPorId(IdProduto);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto para o Id: {IdProduto} não foi encontrado");
            }

            produtoPorId.DescricaoProduto = produto.DescricaoProduto;
     
            _dbContext.Produtos.Update(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return produtoPorId;
        }

        public async Task<bool> Apagar(int IdProduto)
        {
            Produto produtoPorId = await BuscarPorId(IdProduto);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto: {IdProduto} não foi encontrado");
            }

            _dbContext.Produtos.Remove(produtoPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Produto> ObterPorId(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }

    }
}

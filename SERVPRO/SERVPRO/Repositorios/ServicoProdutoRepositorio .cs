using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.Interfaces;

namespace SERVPRO.Repositorios
{
    public class ServicoProdutoRepositorio : IServicoProdutoRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public ServicoProdutoRepositorio(ServproDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Buscar todos os itens de ServicoProduto (todos os produtos de todos os serviços)
        public async Task<List<ServicoProduto>> BuscarTodosServicoProdutos()
        {
            return await _dbContext.ServicoProdutos
                .Include(sp => sp.Servico)   // Inclui os dados do Serviço
                .Include(sp => sp.Produto)   // Inclui os dados do Produto
                .ToListAsync();
        }

        // Buscar um ServicoProduto pelo ID (associação de um produto com um serviço específico)
        public async Task<ServicoProduto> BuscarPorId(int id)
        {
            return await _dbContext.ServicoProdutos
                .Include(sp => sp.Servico)
                .Include(sp => sp.Produto)
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }

        // Buscar ServicoProduto por Serviço e Produto (único produto dentro de um serviço)
        public async Task<ServicoProduto> BuscarPorServicoProduto(int servicoId, int produtoId)
        {
            return await _dbContext.ServicoProdutos
                .Include(sp => sp.Servico)
                .Include(sp => sp.Produto)
                .FirstOrDefaultAsync(sp => sp.ServicoId == servicoId && sp.ProdutoId == produtoId);
        }

        // Adicionar um novo ServicoProduto (associar um produto a um serviço com custo)
        public async Task<ServicoProduto> Adicionar(ServicoProduto servicoProduto)
        {
            // Verifica se a associação entre produto e serviço já existe
            var servicoProdutoExistente = await BuscarPorServicoProduto(servicoProduto.ServicoId, servicoProduto.ProdutoId);
            if (servicoProdutoExistente != null)
            {
                throw new Exception($"O produto {servicoProduto.ProdutoId} já está associado ao serviço {servicoProduto.ServicoId}.");
            }

            // Adiciona a nova associação
            await _dbContext.ServicoProdutos.AddAsync(servicoProduto);
            await _dbContext.SaveChangesAsync();

            return servicoProduto;
        }

        // Atualizar o custo de um produto dentro de um serviço específico
        public async Task<ServicoProduto> AtualizarCustoProdutoNoServico(int servicoId, int produtoId, decimal novoCusto)
        {
            // Busca a associação existente
            var servicoProduto = await BuscarPorServicoProduto(servicoId, produtoId);

            if (servicoProduto == null)
            {
                throw new Exception($"Associação entre serviço {servicoId} e produto {produtoId} não encontrada.");
            }

            // Atualiza o custo do produto dentro do serviço
            servicoProduto.CustoProdutoNoServico = novoCusto;

            // Marca a associação como modificada e salva as alterações no banco de dados
            _dbContext.ServicoProdutos.Update(servicoProduto);
            await _dbContext.SaveChangesAsync();

            return servicoProduto;
        }

        // Remover um ServicoProduto (desassociar um produto de um serviço)
        public async Task<bool> Remover(int servicoId, int produtoId)
        {
            var servicoProduto = await BuscarPorServicoProduto(servicoId, produtoId);

            if (servicoProduto == null)
            {
                throw new Exception($"Associação entre serviço {servicoId} e produto {produtoId} não encontrada.");
            }

            // Remove a associação
            _dbContext.ServicoProdutos.Remove(servicoProduto);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Obter todos os produtos associados a um serviço específico
        public async Task<List<Produto>> ObterProdutosPorServico(int servicoId)
        {
            return await _dbContext.ServicoProdutos
                .Where(sp => sp.ServicoId == servicoId)
                .Include(sp => sp.Produto)  // Inclui os dados do Produto
                .Select(sp => sp.Produto)  // Retorna a lista de produtos
                .ToListAsync();
        }

        // Obter todos os serviços associados a um produto específico
        public async Task<List<Servico>> ObterServicosPorProduto(int produtoId)
        {
            return await _dbContext.ServicoProdutos
                .Where(sp => sp.ProdutoId == produtoId)
                .Include(sp => sp.Servico)  // Inclui os dados do Serviço
                .Select(sp => sp.Servico)  // Retorna a lista de serviços
                .ToListAsync();
        }
    }
}

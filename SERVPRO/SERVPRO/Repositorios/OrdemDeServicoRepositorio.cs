using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class OrdemdeServicoRepositorio : IOrdemDeServicoRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public OrdemdeServicoRepositorio(ServproDBContext servproDBContext)
        {
            _dbContext = servproDBContext;
        }

        public async Task<OrdemDeServico> BuscarPorId(int id)
        {
            return await _dbContext.OrdensDeServico
                .Include(x => x.Cliente)
                .Include(e => e.Equipamento)
                .Include(t => t.Tecnico)
                .Include(t => t.Historicos)
                .Include(t => t.Servicos) 
                .Include(t => t.ServicoProdutos) 
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrdemDeServico>> BuscarTodasOS()
        {
            return await _dbContext.OrdensDeServico
                .Include(x => x.Cliente)
                .Include(x => x.Equipamento)
                .Include(x => x.Tecnico)
                .Include(t => t.Historicos)
                .Include(t => t.Servicos)
                .Include(t => t.ServicoProdutos) 
                .ToListAsync();
        }

        public async Task<OrdemDeServico> Adicionar(OrdemDeServico ordemDeServico)
        {
            ordemDeServico.ValorTotal = CalcularValorTotal(ordemDeServico);
            await _dbContext.OrdensDeServico.AddAsync(ordemDeServico);
            await _dbContext.SaveChangesAsync();

            return ordemDeServico;
        }

        public async Task<OrdemDeServico> Atualizar(OrdemDeServico ordemDeServico, int id)
        {
            OrdemDeServico ordemExistente = await BuscarPorId(id);
            if (ordemExistente == null)
            {
                throw new Exception($"Ordem do Id: {id} não foi encontrada");
            }

            ordemExistente.Status = ordemDeServico.Status;
            ordemExistente.dataConclusao = ordemDeServico.dataConclusao;

            ordemExistente.ValorTotal = CalcularValorTotal(ordemExistente);

            _dbContext.OrdensDeServico.Update(ordemExistente);
            await _dbContext.SaveChangesAsync();

            return ordemExistente;
        }

        public async Task<bool> Apagar(int id)
        {
            OrdemDeServico ordemExistente = await BuscarPorId(id);
            if (ordemExistente == null)
            {
                throw new Exception($"Ordem do Id: {id} não foi encontrada");
            }

            _dbContext.OrdensDeServico.Remove(ordemExistente);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private decimal CalcularValorTotal(OrdemDeServico ordemDeServico)
        {
            decimal totalServicos = ordemDeServico.Servicos?.Sum(s => s.Preco) ?? 0;
            decimal totalProdutos = ordemDeServico.ServicoProdutos?.Sum(sp => sp.PrecoAdicional) ?? 0;

            return totalServicos + totalProdutos;
        }
    }
}

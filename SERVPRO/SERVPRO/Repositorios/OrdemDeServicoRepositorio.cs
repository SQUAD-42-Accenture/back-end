using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SERVPRO.Repositorios
{
    public class OrdemdeServicoRepositorio : IOrdemDeServicoRepositorio
    {
        private readonly ServproDBContext _dbContext;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IServicoRepositorio _servicoRepositorio;

        public OrdemdeServicoRepositorio(ServproDBContext servproDBContext, IProdutoRepositorio produtoRepositorio, IServicoRepositorio servicoRepositorio)
        {
            _dbContext = servproDBContext;
            _produtoRepositorio = produtoRepositorio;
            _servicoRepositorio = servicoRepositorio;
        }

        public async Task<OrdemDeServico> BuscarPorId(int id)
        {
            return await _dbContext.OrdensDeServico
                .Include(x => x.Cliente)
                .Include(e => e.Equipamento)
                .Include(t => t.Tecnico)
                .Include(t => t.Historicos)
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
                .Include(t => t.ServicoProdutos)
                .ToListAsync();
        }

        public async Task<OrdemDeServico> Adicionar(OrdemDeServico ordemDeServico)
        {
            ordemDeServico.ValorTotal = await CalcularValorTotal(ordemDeServico);
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

            ordemExistente.ValorTotal = await CalcularValorTotal(ordemExistente);

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

        private async Task<decimal> CalcularValorTotal(OrdemDeServico ordemDeServico)
        {
            decimal total = 0;

            foreach (var servicoProduto in ordemDeServico.ServicoProdutos)
            {
                var produto = await _produtoRepositorio.ObterPorId(servicoProduto.ProdutoId);
                if (produto != null)
                {
                    total += produto.CustoInterno + servicoProduto.PrecoAdicional;
                }

                var servico = await _servicoRepositorio.ObterPorId(servicoProduto.ServicoId);
                if (servico != null)
                {
                    total += servico.Preco;
                }
            }

            return total;
        }
    }
}

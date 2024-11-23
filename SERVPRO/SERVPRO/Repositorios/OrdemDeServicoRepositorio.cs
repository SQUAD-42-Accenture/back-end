using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.Interfaces;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SERVPRO.Repositorios
{
    public class OrdemDeServicoRepositorio : IOrdemDeServicoRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public OrdemDeServicoRepositorio(ServproDBContext servproDBContext)
        {
            _dbContext = servproDBContext;
        }

        public async Task<List<OrdemDeServico>> BuscarOrdensPorCpfTecnico(string cpf)
        {
            return await _dbContext.OrdensDeServico
                                 .Where(os => os.Tecnico.CPF == cpf) 
                                 .ToListAsync();
        }
        public async Task<OrdemDeServico> Atualizar(int id, OrdemDeServico ordemDeServico)
        {
            var ordemExistente = await _dbContext.OrdensDeServico.FindAsync(id);
            if (ordemExistente == null)
            {
                return null; 
            }

            ordemExistente.Status = ordemDeServico.Status;

            _dbContext.OrdensDeServico.Update(ordemExistente);
            await _dbContext.SaveChangesAsync();

            return ordemExistente;
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

            if (ordemDeServico.ValorTotal == 0)
            {
                Console.WriteLine("Atenção: ValorTotal calculado é 0!");
            }

            await _dbContext.OrdensDeServico.AddAsync(ordemDeServico);
            await _dbContext.SaveChangesAsync();

            return ordemDeServico;
        }
        public async Task<OrdemDeServico> Atualizar(OrdemDeServico ordemDeServico, int id)
        {
            OrdemDeServico ordemExistente = await BuscarPorId(id);
            if (ordemExistente == null)
            {
                throw new Exception($"Ordem de Serviço com ID: {id} não foi encontrada.");
            }

            ordemExistente.Status = ordemDeServico.Status;
            ordemExistente.dataConclusao = ordemDeServico.dataConclusao;

            _dbContext.OrdensDeServico.Update(ordemExistente);
            await _dbContext.SaveChangesAsync();

            return ordemExistente;
        }

        public async Task<bool> Apagar(int id)
        {
            OrdemDeServico ordemExistente = await BuscarPorId(id);
            if (ordemExistente == null)
            {
                throw new Exception($"Ordem de Serviço com ID: {id} não foi encontrada.");
            }

            _dbContext.OrdensDeServico.Remove(ordemExistente);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalcularValorTotal(int id)
        {
            var ordemDeServico = await _dbContext.OrdensDeServico
                .Include(x => x.ServicoProdutos) 
                .ThenInclude(sp => sp.Produto)   
                .ThenInclude(p => p.ServicoProdutos) 
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ordemDeServico == null)
            {
                throw new Exception($"Ordem de Serviço com ID: {id} não foi encontrada.");
            }

            decimal valorTotal = 0;

            if (ordemDeServico.ServicoProdutos != null)
            {
                foreach (var servicoProduto in ordemDeServico.ServicoProdutos)
                {
                    // Se o CustoProdutoNoServico estiver configurado, soma ao valor total
                    valorTotal += servicoProduto.CustoProdutoNoServico;
                }
            }

            // Calculando o valor total com base nos produtos de venda diretamente
            foreach (var produto in ordemDeServico.ServicoProdutos)
            {
                if (produto.Produto != null)
                {
                    valorTotal += produto.Produto.CustoVenda * produto.Produto.Quantidade;
                }
            }

            ordemDeServico.ValorTotal = valorTotal;

            _dbContext.OrdensDeServico.Update(ordemDeServico);
            await _dbContext.SaveChangesAsync();

            return valorTotal;
        }


    }
}

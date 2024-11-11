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

        // Buscar Ordem de Serviço por ID
        public async Task<OrdemDeServico> BuscarPorId(int id)
        {
            return await _dbContext.OrdensDeServico
                .Include(x => x.Cliente)
                .Include(e => e.Equipamento)
                .Include(t => t.Tecnico)
                .Include(t => t.Historicos)
                .Include(t => t.ServicoProdutos)  // Incluir os produtos do serviço
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Buscar Todas as Ordens de Serviço
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

        // Método Adicionar no repositório OrdemDeServicoRepositorio
        public async Task<OrdemDeServico> Adicionar(OrdemDeServico ordemDeServico)
        {
            // Calcular o valor total com base nos serviços e produtos

            // Verificando se o ValorTotal foi calculado corretamente
            if (ordemDeServico.ValorTotal == 0)
            {
                Console.WriteLine("Atenção: ValorTotal calculado é 0!");
            }

            // Adiciona a ordem de serviço ao banco de dados
            await _dbContext.OrdensDeServico.AddAsync(ordemDeServico);
            await _dbContext.SaveChangesAsync();

            return ordemDeServico;
        }



        // Atualizar uma Ordem de Serviço Existente
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

        // Apagar uma Ordem de Serviço
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
                .Include(x => x.ServicoProdutos)  // Incluindo os ServicoProdutos
                .ThenInclude(sp => sp.Produto)    // Incluindo o Produto
                .ThenInclude(p => p.ServicoProdutos) // Incluindo a relação de produtos com serviços
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ordemDeServico == null)
            {
                throw new Exception($"Ordem de Serviço com ID: {id} não foi encontrada.");
            }

            decimal valorTotal = 0;

            // Calculando o valor total baseado nos produtos relacionados à ordem de serviço
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
                    // Produto: considerando a quantidade e o preço de venda
                    valorTotal += produto.Produto.CustoVenda * produto.Produto.Quantidade;
                }
            }

            // Atualizando o campo ValorTotal da OS
            ordemDeServico.ValorTotal = valorTotal;

            // Atualizando no banco de dados
            _dbContext.OrdensDeServico.Update(ordemDeServico);
            await _dbContext.SaveChangesAsync();

            return valorTotal;
        }





    }
}

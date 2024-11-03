using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class ServicoRepositorio : IServicoRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public ServicoRepositorio(ServproDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Servico>> BuscarTodosServicos()
        {
            return await _dbContext.Servicos.ToListAsync();
        }

        public async Task<Servico> BuscarPorId(int id)
        {
            return await _dbContext.Servicos.FindAsync(id);
        }

        public async Task<Servico> Adicionar(Servico servico)
        {
            await _dbContext.Servicos.AddAsync(servico);
            await _dbContext.SaveChangesAsync();
            return servico;
        }

        public async Task<Servico> Atualizar(Servico servico, int id)
        {
            var servicoExistente = await BuscarPorId(id);
            if (servicoExistente == null) throw new Exception($"Serviço não encontrado: {id}");

            servicoExistente.Nome = servico.Nome;
            servicoExistente.Descricao = servico.Descricao;
            servicoExistente.Preco = servico.Preco;

            _dbContext.Servicos.Update(servicoExistente);
            await _dbContext.SaveChangesAsync();
            return servicoExistente;
        }

        public async Task<bool> Apagar(int id)
        {
            var servicoExistente = await BuscarPorId(id);
            if (servicoExistente == null) throw new Exception($"Serviço não encontrado: {id}");

            _dbContext.Servicos.Remove(servicoExistente);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

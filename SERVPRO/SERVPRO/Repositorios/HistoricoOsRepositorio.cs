using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class HistoricoOsRepositorio : IHistoricoOsRepositorio
    {
        private readonly ServproDBContext _dbContext;
        public HistoricoOsRepositorio(ServproDBContext servproDBContext) 
        { 
            _dbContext = servproDBContext;
        }
        public async Task<HistoricoOS> BuscarPorId(int id)
        {
            return await _dbContext.HistoricosOS
                .Include(e => e.OrdemDeServico)
                .Include(t => t.Tecnico)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<HistoricoOS>> BuscarTodoshistoricos()
        {
            return await _dbContext.HistoricosOS
                .Include(x => x.OrdemDeServico)
                .Include(t => t.Tecnico)
                .ToListAsync();
        }
        public async Task<HistoricoOS> Adicionar(HistoricoOS historicoOS)
        {
            _dbContext.HistoricosOS.Add(historicoOS);
            await _dbContext.SaveChangesAsync();
            return historicoOS;
        }
        public async Task<HistoricoOS> Atualizar(HistoricoOS historicoOS, int id)
        {
            var existingHistorico = await _dbContext.HistoricosOS.FindAsync(id);
            if (existingHistorico != null)
            {
                existingHistorico.DataAtualizacao = historicoOS.DataAtualizacao;
                existingHistorico.Comentario = historicoOS.Comentario;
                existingHistorico.TecnicoCPF = historicoOS.TecnicoCPF;

                await _dbContext.SaveChangesAsync();
                return existingHistorico;
            }
            return null;
        }

        public async Task<bool> Apagar(int id)
        {
            HistoricoOS ordemPorId = await BuscarPorId(id);

            if (ordemPorId == null)
            {
                throw new Exception($"Ordem do Id: {id} não foi encontrado");
            }

            _dbContext.HistoricosOS.Remove(ordemPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}

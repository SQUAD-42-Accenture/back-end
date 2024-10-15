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
           await _dbContext.HistoricosOS.AddAsync(historicoOS);
           await _dbContext.SaveChangesAsync();

            return historicoOS;
        }
        public async Task<HistoricoOS> Atualizar(HistoricoOS historicoOS, int id)
        {
            HistoricoOS historicoPorId = await BuscarPorId(id);

            if (historicoPorId == null) 
            {
                throw new Exception($"Ordem do Id: {id} não foi encontrado");
            }

            historicoPorId.Comentario = historicoOS.Comentario;

            _dbContext.HistoricosOS.Update(historicoPorId);
            await _dbContext.SaveChangesAsync();

            return historicoPorId;
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

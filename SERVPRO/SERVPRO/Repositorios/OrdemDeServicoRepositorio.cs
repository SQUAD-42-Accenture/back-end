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
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrdemDeServico>> BuscarTodasOS()
        {
            return await _dbContext.OrdensDeServico
                .Include(x => x.Cliente)
                .Include(x => x.Equipamento)
                .Include(x => x.Tecnico)
                .Include(t => t.Historicos)
                .ToListAsync();
        }
        public async Task<OrdemDeServico> Adicionar(OrdemDeServico ordemDeServico)
        {
           await _dbContext.OrdensDeServico.AddAsync(ordemDeServico);
           await _dbContext.SaveChangesAsync();

            return ordemDeServico;
        }
        public async Task<OrdemDeServico> Atualizar(OrdemDeServico ordemDeServico, int id)
        {
            OrdemDeServico ordemPorId = await BuscarPorId(id);

            if (ordemPorId == null) 
            {
                throw new Exception($"Ordem do Id: {id} não foi encontrado");
            }

            ordemPorId.Status = ordemDeServico.Status;
            ordemPorId.dataConclusao = ordemDeServico.dataConclusao;



            _dbContext.OrdensDeServico.Update(ordemPorId);
            await _dbContext.SaveChangesAsync();

            return ordemPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            OrdemDeServico ordemPorId = await BuscarPorId(id);

            if (ordemPorId == null)
            {
                throw new Exception($"Ordem do Id: {id} não foi encontrado");
            }

            _dbContext.OrdensDeServico.Remove(ordemPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}

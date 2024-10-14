using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class EquipamentoRepositorio : IEquipamentoRepositorio
    {
        private readonly ServproDBContext _dbContext;
        public EquipamentoRepositorio(ServproDBContext servproDBContext) 
        { 
            _dbContext = servproDBContext;
        }
        public async Task<Equipamento> BuscarPorSerial(string serial)
        {
            return await _dbContext.Equipamentos
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.Serial == serial);
        }

        public async Task<List<Equipamento>> BuscarTodosEquipamentos()
        {
            return await _dbContext.Equipamentos
                .Include (x => x.Cliente)
                .ToListAsync();
        }
        

        public async Task<Equipamento> Adicionar(Equipamento equipamento)
        {
           await _dbContext.Equipamentos.AddAsync(equipamento);
           await _dbContext.SaveChangesAsync();

            return equipamento;
        }
        public async Task<Equipamento> Atualizar(Equipamento equipamento, string serial)
        {
            Equipamento equipamentoPorSerial = await BuscarPorSerial(serial);

            if (equipamentoPorSerial == null) 
            {
                throw new Exception($"Usuario para o CPF: {serial} não foi encontrado");
            }

            equipamentoPorSerial.Descricao = equipamento.Descricao;
            equipamentoPorSerial.ClienteCPF = equipamento.ClienteCPF;
            
            
            _dbContext.Equipamentos.Update(equipamentoPorSerial);
            await _dbContext.SaveChangesAsync();

            return equipamentoPorSerial;
        }

        public async Task<bool> Apagar(string serial)
        {
            Equipamento equipamentoPorSerial = await BuscarPorSerial(serial);

            if (equipamentoPorSerial == null)
            {
                throw new Exception($"Equipamento: {serial} não foi encontrado");
            }

            _dbContext.Equipamentos.Remove(equipamentoPorSerial);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}

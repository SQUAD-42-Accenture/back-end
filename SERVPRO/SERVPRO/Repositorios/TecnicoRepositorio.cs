using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class TecnicoRepositorio : ITecnicoRepositorio
    {
        private readonly ServproDBContext _dbContext;
        public TecnicoRepositorio(ServproDBContext servproDBContext) 
        { 
            _dbContext = servproDBContext;
        }
        public async Task<Tecnico> BuscarPorCPF(string cpf)
        {
            return await _dbContext.Tecnicos
                .Include(x => x.OrdensDeServico)
                .FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        public async Task<List<Tecnico>> BuscarTodosTecnicos()
        {
            return await _dbContext.Tecnicos
                .Include(x => x.OrdensDeServico)
                .ToListAsync();
        }
        public async Task<Tecnico> Adicionar(Tecnico tecnico)
        {
           await _dbContext.Tecnicos.AddAsync(tecnico);
           await _dbContext.SaveChangesAsync();

            return tecnico;
        }
        public async Task<Tecnico> Atualizar(Tecnico tecnico, string cpf)
        {
            Tecnico tecnicoPorcPF = await BuscarPorCPF(cpf);

            if (tecnicoPorcPF == null) 
            {
                throw new Exception($"Usuario para o CPF: {cpf} não foi encontrado");
            }

            tecnicoPorcPF.Nome = tecnico.Nome;
            tecnicoPorcPF.Email = tecnico.Email;
            tecnicoPorcPF.Senha = tecnico.Senha;
            tecnicoPorcPF.Especialidade = tecnico.Especialidade;
            tecnicoPorcPF.Telefone = tecnico.Telefone;
            tecnicoPorcPF.StatusTecnico = tecnico.StatusTecnico;

            _dbContext.Tecnicos.Update(tecnicoPorcPF);
            await _dbContext.SaveChangesAsync();

            return tecnicoPorcPF;
        }

        public async Task<bool> Apagar(string cpf)
        {
            Tecnico tecnicoPorcPF = await BuscarPorCPF(cpf);

            if (tecnicoPorcPF == null)
            {
                throw new Exception($"Usuario para o CPF: {cpf} não foi encontrado");
            }

            _dbContext.Tecnicos.Remove(tecnicoPorcPF);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}

using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class AdministradorRepositorio : IAdministradorRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public AdministradorRepositorio(ServproDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Administrador>> BuscarTodosAdministradores()
        {
            return await _dbContext.Administradores.ToListAsync();
        }

        public async Task<Administrador> BuscarPorCPF(string cpf)
        {
            return await _dbContext.Administradores.FirstOrDefaultAsync(a => a.CPF == cpf);
        }

        public async Task<Administrador> Adicionar(Administrador administrador)
        {
            await _dbContext.Administradores.AddAsync(administrador);
            await _dbContext.SaveChangesAsync();
            return administrador;
        }

        public async Task<Administrador> Atualizar(Administrador administrador, string cpf)
        {
            var adminExistente = await BuscarPorCPF(cpf);
            if (adminExistente == null)
            {
                throw new Exception($"Administrador com CPF {cpf} não encontrado.");
            }
            // Atualize as propriedades do adminExistente com as do administrador
            adminExistente.Nome = administrador.Nome;
            adminExistente.Email = administrador.Email;
            adminExistente.Telefone = administrador.Telefone;
            adminExistente.Departamento = administrador.Departamento;
            adminExistente.DataContratacao = administrador.DataContratacao;

            _dbContext.Administradores.Update(adminExistente);
            await _dbContext.SaveChangesAsync();
            return adminExistente;
        }

        public async Task<bool> Apagar(string cpf)
        {
            var adminExistente = await BuscarPorCPF(cpf);
            if (adminExistente == null)
            {
                throw new Exception($"Administrador com CPF {cpf} não encontrado.");
            }

            _dbContext.Administradores.Remove(adminExistente);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

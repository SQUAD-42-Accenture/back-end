using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ServproDBContext _dbContext;

        public ClienteRepositorio(ServproDBContext servproDBContext)
        {
            _dbContext = servproDBContext;
        }

        public async Task<Cliente> BuscarPorCPF(string cpf)
        {
            return await _dbContext.Clientes
                .Include(x => x.Equipamentos)
                .FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        public async Task<List<Cliente>> BuscarTodosClientes()
        {
            return await _dbContext.Clientes
                .Include(x => x.Equipamentos)
                .ToListAsync();
        }

        public async Task<Cliente> Atualizar(Cliente cliente, string cpf)
        {
            Cliente clientePorcPF = await BuscarPorCPF(cpf);

            if (clientePorcPF == null)
            {
                throw new Exception($"Usuário para o CPF: {cpf} não foi encontrado");
            }

            clientePorcPF.Nome = cliente.Nome;
            clientePorcPF.Telefone = cliente.Telefone;
            clientePorcPF.Email = cliente.Email;
            clientePorcPF.Senha = cliente.Senha;
            clientePorcPF.CEP = cliente.CEP;
            clientePorcPF.Bairro = cliente.Bairro;
            clientePorcPF.Cidade = cliente.Cidade;
            clientePorcPF.DataNascimento = cliente.DataNascimento;
            clientePorcPF.Complemento = cliente.Complemento;


            _dbContext.Clientes.Update(clientePorcPF);
            await _dbContext.SaveChangesAsync();

            return clientePorcPF;
        }
        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> Apagar(string cpf)
        {
            Cliente clientePorcPF = await BuscarPorCPF(cpf);

            if (clientePorcPF == null)
            {
                throw new Exception($"Usuário para o CPF: {cpf} não foi encontrado");
            }

            _dbContext.Clientes.Remove(clientePorcPF);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

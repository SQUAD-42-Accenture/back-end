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
            // Buscar o cliente no banco de dados
            Cliente clientePorcPF = await BuscarPorCPF(cpf);

            if (clientePorcPF == null)
            {
                throw new Exception($"Usuário para o CPF: {cpf} não foi encontrado");
            }

            // Atualizar os campos somente se o valor não for nulo
            if (!string.IsNullOrEmpty(cliente.Nome)) clientePorcPF.Nome = cliente.Nome;
            if (!string.IsNullOrEmpty(cliente.Telefone)) clientePorcPF.Telefone = cliente.Telefone;
            if (!string.IsNullOrEmpty(cliente.Email)) clientePorcPF.Email = cliente.Email;
            if (!string.IsNullOrEmpty(cliente.Senha)) clientePorcPF.Senha = cliente.Senha;
            if (!string.IsNullOrEmpty(cliente.CEP)) clientePorcPF.CEP = cliente.CEP;
            if (!string.IsNullOrEmpty(cliente.Bairro)) clientePorcPF.Bairro = cliente.Bairro;
            if (!string.IsNullOrEmpty(cliente.Cidade)) clientePorcPF.Cidade = cliente.Cidade;
            if (!string.IsNullOrEmpty(cliente.Complemento)) clientePorcPF.Complemento = cliente.Complemento;

            // Se você não quer atualizar o campo DataNascimento, deixe-o de fora

            // Atualizar no banco
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

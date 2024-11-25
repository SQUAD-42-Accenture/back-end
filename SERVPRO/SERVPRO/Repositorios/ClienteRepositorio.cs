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

        public async Task<Cliente> Atualizar( Cliente clienteAtualizado, string cpf)
        {
            Cliente clienteExistente = await BuscarPorCPF(cpf);
            if (clienteExistente == null)
            {
                throw new Exception($"Cliente com CPF {cpf} não encontrado.");
            }

            // Atualiza os campos, mas apenas se não forem nulos ou vazios
            if (!string.IsNullOrEmpty(clienteAtualizado.Nome))
            {
                clienteExistente.Nome = clienteAtualizado.Nome;
            }
            if (!string.IsNullOrEmpty(clienteAtualizado.Telefone))
            {
                clienteExistente.Telefone = clienteAtualizado.Telefone;
            }

            if (!string.IsNullOrEmpty(clienteAtualizado.Email))
            {
                clienteExistente.Email = clienteAtualizado.Email;
            }

            if (!string.IsNullOrEmpty(clienteAtualizado.CEP))
            {
                clienteExistente.CEP = clienteAtualizado.CEP;
            }

            if (!string.IsNullOrEmpty(clienteAtualizado.Bairro))
            {
                clienteExistente.Bairro = clienteAtualizado.Bairro;
            }

            if (!string.IsNullOrEmpty(clienteAtualizado.Cidade))
            {
                clienteExistente.Cidade = clienteAtualizado.Cidade;
            }

            if (!string.IsNullOrEmpty(clienteAtualizado.Complemento))
            {
                clienteExistente.Complemento = clienteAtualizado.Complemento;
            }



            // Atualiza no banco de dados
            _dbContext.Clientes.Update(clienteExistente);
            await _dbContext.SaveChangesAsync();

            return clienteExistente;
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

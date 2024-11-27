using SERVPRO.Models;
using SERVPRO.Repositorios;
using SERVPRO.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;  // Adicionado para incluir Guid e DateTime

namespace SERVPRO.Tests
{
    public class ClienteRepositorioTest
    {
        private DbContextOptions<ServproDBContext> _options;

        public ClienteRepositorioTest()
        {
            _options = new DbContextOptionsBuilder<ServproDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  // Usando Guid 
                .Options;
        }

        [Fact]
        public async Task Atualizar_Cliente_DeveAtualizarDadosCorretamente()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new ClienteRepositorio(context);

                var cliente = new Cliente
                {
                    CPF = "12345678901",
                    Nome = "Cliente Teste",
                    Email = "cliente@teste.com",
                    Telefone = "123456789",
                    CEP = "12345-678",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Complemento = "Apt. 101",
                    Senha = "senha123",
                    TipoUsuario = "Comum",
                    DataNascimento = new DateTime(1990, 1, 1)  // Usando DateTime 
                };

                await repositorio.Adicionar(cliente);

                cliente.Senha = "novaSenha123";
                cliente.TipoUsuario = "Admin";
                cliente.DataNascimento = new DateTime(1985, 5, 15);  

                var clienteAtualizado = await repositorio.Atualizar(cliente, cliente.CPF);

                Assert.NotNull(clienteAtualizado);
                Assert.Equal("novaSenha123", clienteAtualizado.Senha);
                Assert.Equal("Admin", clienteAtualizado.TipoUsuario);
                Assert.Equal(new DateTime(1985, 5, 15), clienteAtualizado.DataNascimento);  
            }
        }

        [Fact]
        public async Task BuscarTodosClientes_DeveRetornarTodosClientes()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new ClienteRepositorio(context);

                await repositorio.Adicionar(new Cliente
                {
                    CPF = "12345678901",
                    Nome = "Cliente 1",
                    Email = "cliente1@teste.com",
                    Telefone = "123456789",
                    CEP = "12345-678",
                    Bairro = "Bairro Teste 1",
                    Cidade = "Cidade Teste 1",
                    Complemento = "Apt. 101",
                    Senha = "senha123",
                    TipoUsuario = "Comum",
                    DataNascimento = new DateTime(1990, 1, 1)  
                });

                await repositorio.Adicionar(new Cliente
                {
                    CPF = "23456789012",
                    Nome = "Cliente 2",
                    Email = "cliente2@teste.com",
                    Telefone = "234567890",
                    CEP = "23456-789",
                    Bairro = "Bairro Teste 2",
                    Cidade = "Cidade Teste 2",
                    Complemento = "Apt. 102",
                    Senha = "senha123",
                    TipoUsuario = "Comum",
                    DataNascimento = new DateTime(1995, 2, 2)  
                });

                var clientes = await context.Clientes.ToListAsync(); 

                Assert.NotNull(clientes);
                Assert.Equal(2, clientes.Count);
            }
        }

        [Fact]
        public async Task BuscarPorCPF_ClienteExistente_DeveRetornarCliente()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new ClienteRepositorio(context);

                var cliente = new Cliente
                {
                    CPF = "12345678901",
                    Nome = "Cliente Teste",
                    Email = "cliente@teste.com",
                    Telefone = "123456789",
                    CEP = "12345-678",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Complemento = "Apt. 101",
                    Senha = "senha123",
                    TipoUsuario = "Comum",
                    DataNascimento = new DateTime(1990, 1, 1) 
                };

                await repositorio.Adicionar(cliente);

                var clienteEncontrado = await repositorio.BuscarPorCPF("12345678901");

                Assert.NotNull(clienteEncontrado);
                Assert.Equal(cliente.CPF, clienteEncontrado.CPF);
            }
        }

        [Fact]
        public async Task Adicionar_Cliente_DeveAdicionarNoBanco()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new ClienteRepositorio(context);

                var cliente = new Cliente
                {
                    CPF = "12345678901",
                    Nome = "Cliente Teste",
                    Email = "cliente@teste.com",
                    Telefone = "123456789",
                    CEP = "12345-678",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Complemento = "Apt. 101",
                    Senha = "senha123",
                    TipoUsuario = "Comum",
                    DataNascimento = new DateTime(1990, 1, 1)  
                };

                var clienteAdicionado = await repositorio.Adicionar(cliente);

                Assert.NotNull(clienteAdicionado);
                Assert.Equal(cliente.CPF, clienteAdicionado.CPF);
            }
        }

        [Fact]
        public async Task Apagar_ClienteExistente_DeveRemoverCliente()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new ClienteRepositorio(context);

                var cliente = new Cliente
                {
                    CPF = "12345678901",
                    Nome = "Cliente Teste",
                    Email = "cliente@teste.com",
                    Telefone = "123456789",
                    CEP = "12345-678",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Complemento = "Apt. 101",
                    Senha = "senha123",
                    TipoUsuario = "Comum",
                    DataNascimento = new DateTime(1990, 1, 1)  
                };

                await repositorio.Adicionar(cliente);

                await repositorio.Apagar(cliente.CPF);

                var clienteRemovido = await repositorio.BuscarPorCPF(cliente.CPF);
                Assert.Null(clienteRemovido);
            }
        }
    }
}

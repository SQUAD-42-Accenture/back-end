using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;
using SERVPRO.Repositorios;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SERVPRO.Tests
{
    public class AdministradorRepositorioTest
    {
        private DbContextOptions<ServproDBContext> _options;

        public AdministradorRepositorioTest()
        {
            _options = new DbContextOptionsBuilder<ServproDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task Adicionar_Administrador_DeveAdicionarNoBanco()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new AdministradorRepositorio(context);

                var administrador = new Administrador
                {
                    CPF = "12345678901",
                    Nome = "Administrador Teste",
                    Email = "admin@teste.com",
                    Telefone = "123456789",
                    Departamento = "TI",
                    DataContratacao = new DateTime(2020, 5, 1),
                    Senha = "Senha123", 
                    TipoUsuario = "Administrador" 
                };

                var adminAdicionado = await repositorio.Adicionar(administrador);

                Assert.NotNull(adminAdicionado);
                Assert.Equal(administrador.CPF, adminAdicionado.CPF);
            }
        }

        [Fact]
        public async Task BuscarTodosAdministradores_DeveRetornarTodosAdministradores()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new AdministradorRepositorio(context);

                await repositorio.Adicionar(new Administrador
                {
                    CPF = "12345678901",
                    Nome = "Administrador 1",
                    Email = "admin1@teste.com",
                    Telefone = "123456789",
                    Departamento = "TI",
                    DataContratacao = new DateTime(2020, 5, 1),
                    Senha = "Senha123",
                    TipoUsuario = "Administrador"
                });

                await repositorio.Adicionar(new Administrador
                {
                    CPF = "23456789012",
                    Nome = "Administrador 2",
                    Email = "admin2@teste.com",
                    Telefone = "234567890",
                    Departamento = "RH",
                    DataContratacao = new DateTime(2021, 6, 15),
                    Senha = "Senha123",
                    TipoUsuario = "Administrador"
                });

                var administradores = await repositorio.BuscarTodosAdministradores();

                Assert.NotNull(administradores);
                Assert.Equal(2, administradores.Count);
            }
        }

        [Fact]
        public async Task BuscarPorCPF_AdministradorExistente_DeveRetornarAdministrador()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new AdministradorRepositorio(context);

                var administrador = new Administrador
                {
                    CPF = "12345678901",
                    Nome = "Administrador Teste",
                    Email = "admin@teste.com",
                    Telefone = "123456789",
                    Departamento = "TI",
                    DataContratacao = new DateTime(2020, 5, 1),
                    Senha = "Senha123",
                    TipoUsuario = "Administrador"
                };

                await repositorio.Adicionar(administrador);

                var adminEncontrado = await repositorio.BuscarPorCPF("12345678901");

                Assert.NotNull(adminEncontrado);
                Assert.Equal(administrador.CPF, adminEncontrado.CPF);
            }
        }

        [Fact]
        public async Task Atualizar_Administrador_DeveAtualizarDadosCorretamente()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new AdministradorRepositorio(context);

                var administrador = new Administrador
                {
                    CPF = "12345678901",
                    Nome = "Administrador Teste",
                    Email = "admin@teste.com",
                    Telefone = "123456789",
                    Departamento = "TI",
                    DataContratacao = new DateTime(2020, 5, 1),
                    Senha = "Senha123",
                    TipoUsuario = "Administrador"
                };

                await repositorio.Adicionar(administrador);

                administrador.Nome = "Administrador Atualizado";
                administrador.Departamento = "Financeiro";
                administrador.DataContratacao = new DateTime(2021, 1, 1);

                var adminAtualizado = await repositorio.Atualizar(administrador, administrador.CPF);

                Assert.NotNull(adminAtualizado);
                Assert.Equal("Administrador Atualizado", adminAtualizado.Nome);
                Assert.Equal("Financeiro", adminAtualizado.Departamento);
                Assert.Equal(new DateTime(2021, 1, 1), adminAtualizado.DataContratacao);
            }
        }

        [Fact]
        public async Task Apagar_AdministradorExistente_DeveRemoverAdministrador()
        {
            using (var context = new ServproDBContext(_options))
            {
                var repositorio = new AdministradorRepositorio(context);

                var administrador = new Administrador
                {
                    CPF = "12345678901",
                    Nome = "Administrador Teste",
                    Email = "admin@teste.com",
                    Telefone = "123456789",
                    Departamento = "TI",
                    DataContratacao = new DateTime(2020, 5, 1),
                    Senha = "Senha123",
                    TipoUsuario = "Administrador"
                };

                await repositorio.Adicionar(administrador);

                var foiRemovido = await repositorio.Apagar(administrador.CPF);

                Assert.True(foiRemovido);

                var adminRemovido = await repositorio.BuscarPorCPF(administrador.CPF);
                Assert.Null(adminRemovido); 
            }
        }
    }
}

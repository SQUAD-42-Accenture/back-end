using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;
using SERVPRO.Data;
using SERVPRO.Repositorios;
using Xunit;

namespace SERVPRO.Tests
{
    public class UsuarioRepositorioTest : IDisposable
    {
        private readonly ServproDBContext _dbContext;
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly Random _random;

        public UsuarioRepositorioTest()
        {
            var options = new DbContextOptionsBuilder<ServproDBContext>()
                .UseInMemoryDatabase(databaseName: "TestServproDb")
                .Options;

            _dbContext = new ServproDBContext(options);
            _usuarioRepositorio = new UsuarioRepositorio(_dbContext);
            _random = new Random(); 

            AdicionarUsuariosIniciais();
        }

        private void AdicionarUsuariosIniciais()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    CPF = GerarCpfAleatorio(),
                    Nome = GerarNomeAleatorio(),
                    Email = GerarEmailAleatorio(),
                    Senha = "senha" + _random.Next(1000, 9999), 
                    TipoUsuario = "Tecnico"
                },
                new Usuario
                {
                    CPF = GerarCpfAleatorio(),
                    Nome = GerarNomeAleatorio(),
                    Email = GerarEmailAleatorio(),
                    Senha = "senha" + _random.Next(1000, 9999),
                    TipoUsuario = "Admin"
                }
            };

            _dbContext.Usuarios.AddRange(usuarios);
            _dbContext.SaveChanges();
        }

        private string GerarCpfAleatorio()
        {
            return _random.Next(100000000, 999999999).ToString() + _random.Next(10, 99).ToString();
        }

        private string GerarNomeAleatorio()
        {
            var nomes = new List<string>
            {
                "João", "Maria", "Carlos", "Ana", "Lucas", "Fernanda", "Beatriz", "Pedro", "Cláudia", "Rafael"
            };
            var sobrenomes = new List<string>
            {
                "Silva", "Oliveira", "Santos", "Pereira", "Souza", "Costa", "Almeida", "Rodrigues", "Lima", "Martins"
            };

            var nome = nomes[_random.Next(nomes.Count)] + " " + sobrenomes[_random.Next(sobrenomes.Count)];
            return nome;
        }

        private string GerarEmailAleatorio()
        {
            var dominio = "teste.com";
            var nomeUsuario = Guid.NewGuid().ToString("N").Substring(0, 8); 
            return nomeUsuario + "@" + dominio;
        }

        [Fact]
        public async Task BuscarPorCpf_DeveRetornarUsuarioExistente()
        {

            var cpf = _dbContext.Usuarios.First().CPF;

            var usuario = await _usuarioRepositorio.BuscarPorCpf(cpf);

            Assert.NotNull(usuario);
            Assert.Equal(cpf, usuario.CPF);
        }

        [Fact]
        public async Task BuscarPorTipoUsuario_DeveRetornarUsuariosDeTipoCorreto()
        {
            var tipoUsuario = "Tecnico";

            var usuarios = await _usuarioRepositorio.BuscarPorTipoUsuario(tipoUsuario);

            Assert.NotEmpty(usuarios);
            Assert.All(usuarios, u => Assert.Equal(tipoUsuario, u.TipoUsuario));
        }

        [Fact]
        public async Task BuscarTodosUsuarios_DeveRetornarTodosOsUsuarios()
        {
            var usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();

            Assert.NotEmpty(usuarios);
            Assert.Equal(2, usuarios.Count); 
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

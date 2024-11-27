using SERVPRO.Models;
using SERVPRO.Repositorios;
using SERVPRO.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SERVPRO.Tests
{
    public class TecnicoRepositorioTest
    {
        private readonly DbContextOptions<ServproDBContext> _dbContextOptions;

        public TecnicoRepositorioTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ServproDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ServproDBContext(_dbContextOptions))
            {
                if (!context.Tecnicos.Any()) 
                {
                    context.Tecnicos.AddRange(
                        new Tecnico { 
                            CPF = "12345678901", 
                            Nome = "Técnico 1", 
                            Especialidade = "Elétrica",
                            Email = "tecnico1@empresa.com",
                            Senha = "senha123",
                            TipoUsuario = "Tecnico",
                            StatusTecnico = "Ativo",
                            Telefone = "123456789"
                        },
                        new Tecnico { 
                            CPF = "10987654321", 
                            Nome = "Técnico 2", 
                            Especialidade = "Hidráulica",
                            Email = "tecnico2@empresa.com",
                            Senha = "senha456",
                            TipoUsuario = "Tecnico",
                            StatusTecnico = "Ativo",
                            Telefone = "987654321"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task BuscarPorCpf_TecnicoExistente_DeveRetornarTecnico()
        {
            using (var context = new ServproDBContext(_dbContextOptions))
            {
                var repositorio = new TecnicoRepositorio(context);

                var tecnico = await repositorio.BuscarPorCPF("12345678901");

                Assert.NotNull(tecnico);
                Assert.Equal("Técnico 1", tecnico.Nome); 
                Assert.Equal("Elétrica", tecnico.Especialidade); 
            }
        }

        [Fact]
        public async Task BuscarTodosTecnicos_DeveRetornarTodosTecnicos()
        {
            using (var context = new ServproDBContext(_dbContextOptions))
            {
                var repositorio = new TecnicoRepositorio(context);

                var tecnicos = await repositorio.BuscarTodosTecnicos();

                Assert.NotEmpty(tecnicos); 
                Assert.Equal(2, tecnicos.Count); 
            }
        }

        [Fact]
        public async Task Adicionar_Tecnico_DeveAdicionarNoBanco()
        {
            using (var context = new ServproDBContext(_dbContextOptions))
            {
                var repositorio = new TecnicoRepositorio(context);

                var novoTecnico = new Tecnico
                {
                    CPF = "11223344556",
                    Nome = "Novo Técnico",
                    Especialidade = "Serralheria",
                    Email = "novo@tecnico.com",  
                    Senha = "senha123",          
                    TipoUsuario = "Tecnico",    
                    StatusTecnico = "Ativo",   
                    Telefone = "123456789"      
                };

                await repositorio.Adicionar(novoTecnico);

                var tecnico = await context.Tecnicos.FirstOrDefaultAsync(t => t.CPF == "11223344556");

                Assert.NotNull(tecnico); 
                Assert.Equal("Novo Técnico", tecnico.Nome); 
                Assert.Equal("Serralheria", tecnico.Especialidade); 
            }
        }

        [Fact]
        public async Task Apagar_Tecnico_Existente_DeveRemoverTecnico()
        {
            using (var context = new ServproDBContext(_dbContextOptions))
            {
                var repositorio = new TecnicoRepositorio(context);

                var resultado = await repositorio.Apagar("12345678901");

                Assert.True(resultado);

                var tecnicoRemovido = await context.Tecnicos.FirstOrDefaultAsync(t => t.CPF == "12345678901");

                Assert.Null(tecnicoRemovido); 
            }
        }
    }
}

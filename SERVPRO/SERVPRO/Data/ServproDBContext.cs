using Microsoft.EntityFrameworkCore;
using SERVPRO.Data.Map;
using SERVPRO.Enums;
using SERVPRO.Models;
using System;

namespace SERVPRO.Data
{
    public class ServproDBContext : DbContext
    {
        public ServproDBContext(DbContextOptions<ServproDBContext> options)
            : base(options)
        { 

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<OrdemDeServico> OrdensDeServico { get; set; }
        public DbSet<HistoricoOS> HistoricosOS { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ServicoProduto> ServicoProdutos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new TecnicoMap());
            modelBuilder.ApplyConfiguration(new AdministradorMap());
            modelBuilder.ApplyConfiguration(new EquipamentoMap());
            modelBuilder.ApplyConfiguration(new OrdemDeServicoMap());
            modelBuilder.ApplyConfiguration(new HistoricoOsMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ServicoMap());
            modelBuilder.ApplyConfiguration(new ServicoProdutoMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}

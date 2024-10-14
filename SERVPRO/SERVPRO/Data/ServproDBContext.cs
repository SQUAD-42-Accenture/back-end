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

       // public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        //public DbSet<OrdemDeServico> OrdensDeServico { get; set; }
        //public DbSet<HistoricoOS> HistoricosOS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
        .HasDiscriminator<string>("TipoUsuario")
        .HasValue<Cliente>("Cliente")
        .HasValue<Tecnico>("Tecnico")
        .HasValue<Administrador>("Administrador");


            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new TecnicoMap());
            modelBuilder.ApplyConfiguration(new AdministradorMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new EquipamentoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

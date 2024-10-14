using Microsoft.EntityFrameworkCore;
using SERVPRO.Data.Map;
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

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        //public DbSet<Tecnico> Tecnicos { get; set; }
        //public DbSet<OrdemDeServico> OrdensDeServico { get; set; }
        //public DbSet<HistoricoOS> HistoricosOS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EquipamentoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

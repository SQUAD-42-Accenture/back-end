using Microsoft.EntityFrameworkCore;
using SERVPRO.API.User.Models;
using SERVPRO.API.Tecnico.Models;
using SERVPRO.API.Cliente.Models;
using SERVPRO.API.Admin.Models;

namespace SERVPRO.API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TecnicoModels> Tecnicos { get; set; }
        public DbSet<ClienteModels> Clientes { get; set; }
        public DbSet<AdminModels> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar o ID como chave primária para UserModel e suas subclasses
            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.Id);
        }
    }
}

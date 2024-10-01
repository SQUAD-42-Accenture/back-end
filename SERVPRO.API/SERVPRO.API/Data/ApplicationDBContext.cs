using Microsoft.EntityFrameworkCore;
using SERVPRO.API.Models;

namespace SERVPRO.API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserLoginModel> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

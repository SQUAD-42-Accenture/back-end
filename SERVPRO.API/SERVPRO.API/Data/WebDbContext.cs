using Microsoft.EntityFrameworkCore;
using SERVPRO.API.Helpers;
using SERVPRO.API.Models;
using System;

namespace SERVPRO.API.Data
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserLoginModel> UsersLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    Name = "Ellen Peixoto",
                    Email = "ellen_SERVPRO@gmail.com",
                    Password = PasswordHelper.HashPassword("Senha@123"),
                    Role = "Admin",
                    Cpf = "12345678900",
                    ContactNumber = "987654321" 
                }
            );
        }
    }
}

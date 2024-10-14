using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class AdministradorMap : IEntityTypeConfiguration<Administrador>
    {
        public void Configure(EntityTypeBuilder<Administrador> builder)
        {
            //builder.HasKey(x => x.CPF);
           // builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
//builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            //builder.Property(x => x.Senha).IsRequired().HasMaxLength(255);
            //builder.Property(x => x.TipoUsuario).IsRequired();





        }
    }
}

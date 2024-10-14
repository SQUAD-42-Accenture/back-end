using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class TecnicoMap : IEntityTypeConfiguration<Tecnico>
    {
        public void Configure(EntityTypeBuilder<Tecnico> builder)
        {
            //builder.HasKey(x => x.CPF);
           // builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Especialidade).IsRequired().HasMaxLength(255);
           // builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            //builder.Property(x => x.Senha).IsRequired().HasMaxLength(255);
            //builder.Property(x => x.TipoUsuario).IsRequired();





        }
    }
}

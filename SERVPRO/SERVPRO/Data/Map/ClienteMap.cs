using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            //builder.HasKey(x => x.CPF);
            //builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(11);
            builder.Property(x => x.CEP).IsRequired().HasMaxLength(10); 
            builder.Property(x => x.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DataNascimento).IsRequired();
            //builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            //uilder.Property(x => x.Senha).IsRequired().HasMaxLength(255);
            //builder.Property(x => x.TipoUsuario).IsRequired();



        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class EquipamentoMap : IEntityTypeConfiguration<Equipamento>
    {
        public void Configure(EntityTypeBuilder<Equipamento> builder)
        {
            builder.HasKey(x => x.Serial);
            builder.Property(x => x.Marca).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Modelo).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.ClienteCPF);

            builder.HasOne(x => x.Cliente)
              .WithMany(c => c.Equipamentos) 
              .HasForeignKey(x => x.ClienteCPF)  
              .OnDelete(DeleteBehavior.Restrict); 


        }
    }
}

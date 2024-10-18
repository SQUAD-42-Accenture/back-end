using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class HistoricoOsMap : IEntityTypeConfiguration<HistoricoOS>
    {
        public void Configure(EntityTypeBuilder<HistoricoOS> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Comentario).IsRequired();
            builder.Property(x => x.DataAtualizacao).IsRequired().HasMaxLength(255);
            builder.Property(x => x.OrdemDeServicoId).IsRequired(false);
            builder.Property(x => x.TecnicoCPF).IsRequired(false);



            builder.HasOne(x => x.Tecnico)
              .WithMany()
              .HasForeignKey(x => x.TecnicoCPF)  
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrdemDeServico)
              .WithMany(x => x.Historicos)
              .HasForeignKey(x => x.OrdemDeServicoId)
              .OnDelete(DeleteBehavior.Restrict);


            



        }
    }
}

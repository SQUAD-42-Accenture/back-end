using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class OrdemDeServicoMap : IEntityTypeConfiguration<OrdemDeServico>
    {
        public void Configure(EntityTypeBuilder<OrdemDeServico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(255);
            builder.Property(x => x.dataAbertura).IsRequired();
            builder.Property(x => x.dataConclusao).IsRequired(false);
            builder.Property(x => x.ClienteCPF).IsRequired(false);
            builder.Property(x => x.TecnicoCPF).IsRequired(false);
            builder.Property(x => x.SerialEquipamento).IsRequired(false);
            builder.Property(x => x.MetodoPagamento).IsRequired(false);
            builder.Property(x => x.ValorTotal).IsRequired(false);



            builder.HasOne(x => x.Cliente)
              .WithMany(c => c.OrdensDeServico)
              .HasForeignKey(x => x.ClienteCPF)  
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Equipamento)
              .WithMany()
              .HasForeignKey(x => x.SerialEquipamento)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tecnico)
              .WithMany(t => t.OrdensDeServico)
              .HasForeignKey(x => x.TecnicoCPF)
              .OnDelete(DeleteBehavior.Restrict);



            



        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Descricao).HasMaxLength(255);
           // builder.Property(x => x.Preco).HasColumnType("decimal(10, 2)").IsRequired();
        }
    }
}

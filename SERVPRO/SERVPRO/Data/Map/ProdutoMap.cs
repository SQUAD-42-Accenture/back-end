using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;

namespace SERVPRO.Data.Map {
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Categoria).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CustoInterno).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(x => x.CustoVenda).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(x => x.Quantidade).IsRequired();
            builder.Property(x => x.DataEntrada).IsRequired();
        }
    }

}

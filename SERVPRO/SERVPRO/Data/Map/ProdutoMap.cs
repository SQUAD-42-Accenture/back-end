using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;

namespace SERVPRO.Data.Map {
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NomeProduto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DescricaoProduto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CategoriaProduto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CustoInterno).IsRequired();
            builder.Property(x => x.Quantidade).IsRequired();
            builder.Property(x => x.DataEntrada).IsRequired();
        }
    }

}

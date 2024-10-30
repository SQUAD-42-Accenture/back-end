using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;

namespace SERVPRO.Data.Map {
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.IdProduto);
            builder.Property(x => x.NomeProduto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DescricaoProduto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CategoriaProduto).IsRequired().HasMaxLength(16);
            builder.Property(x => x.PrecoProduto).IsRequired();
            builder.Property(x => x.QtdProduto).IsRequired();
            builder.Property(x => x.DataEntrada).IsRequired();
        }
    }

}

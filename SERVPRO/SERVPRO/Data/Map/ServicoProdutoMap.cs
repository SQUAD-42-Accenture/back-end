using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class ServicoProdutoMap : IEntityTypeConfiguration<ServicoProduto>
    {
        public void Configure(EntityTypeBuilder<ServicoProduto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProdutoId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ServicoId).IsRequired().HasMaxLength(100);

            
            builder.HasOne(sp => sp.Servico)
                .WithMany(s => s.ServicoProdutos)
                .HasForeignKey(sp => sp.ServicoId);

            builder.HasOne(sp => sp.Produto)
                .WithMany()
                .HasForeignKey(sp => sp.ProdutoId);

        }
    }
}

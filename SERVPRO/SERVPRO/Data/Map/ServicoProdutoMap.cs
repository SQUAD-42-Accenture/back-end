using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;

namespace SERVPRO.Data.Map
{
    public class ServicoProdutoMap : IEntityTypeConfiguration<ServicoProduto>
    {
        public void Configure(EntityTypeBuilder<ServicoProduto> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.HasOne(sp => sp.Servico)
                .WithMany(s => s.ServicoProdutos)
                .HasForeignKey(sp => sp.ServicoId);

            builder.HasOne(sp => sp.Produto)
                .WithMany()
                .HasForeignKey(sp => sp.ProdutoId);
        }
    }
}

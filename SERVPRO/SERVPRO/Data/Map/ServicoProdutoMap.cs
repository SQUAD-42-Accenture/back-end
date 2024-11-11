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

            builder.HasOne(sp => sp.Servico)
                .WithMany(s => s.ServicoProdutos)
                .HasForeignKey(sp => sp.ServicoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.Produto)
                .WithMany(p => p.ServicoProdutos)
                .HasForeignKey(sp => sp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com a entidade OrdemDeServico
            builder.HasOne(sp => sp.OrdemDeServico) // Definindo o relacionamento com OrdemDeServico
                .WithMany(os => os.ServicoProdutos) // A OrdemDeServico pode ter muitos ServicoProdutos
                .HasForeignKey(sp => sp.OrdemDeServicoId) // Usando a chave estrangeira OrdemDeServicoId
                .OnDelete(DeleteBehavior.Cascade); // Comportamento de deleção em cascata (opcional)

        }
    }
}

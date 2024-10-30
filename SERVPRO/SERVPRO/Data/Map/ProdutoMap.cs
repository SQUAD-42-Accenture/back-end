namespace SERVPRO.Data.Map
{
    public class ProdutoMap
    {
        builder.HasKey(x => x.IdProduto).IsRequired().HasMaxLength(6);;
        builder.Property(x => x.NomeProduto).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DescricaoProduto).IsnotRequired().HasMaxLength(100);
        builder.Property(x => x.CategoriaProduto).IsRequired().HasMaxLength(16);
        builder.Property(x => x.PrecoProduto).IsRequired();
        builder.Property(x => x.QtdProduto).IsRequired();
        builder.Property(x => x.DataEntrada).IsRequired();

    }
}

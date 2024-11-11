using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoDosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoId",
                table: "OrdensDeServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdensDeServico_ProdutoId",
                table: "OrdensDeServico");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "CustoAssociadoServico",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "IdProduto",
                table: "OrdensDeServico");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "OrdensDeServico");

            migrationBuilder.RenameColumn(
                name: "PrecoAdicional",
                table: "ServicoProdutos",
                newName: "CustoProdutoNoServico");

            migrationBuilder.RenameColumn(
                name: "NomeProduto",
                table: "Produtos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DescricaoProduto",
                table: "Produtos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CustoVendaCliente",
                table: "Produtos",
                newName: "CustoVenda");

            migrationBuilder.RenameColumn(
                name: "CategoriaProduto",
                table: "Produtos",
                newName: "Categoria");

            migrationBuilder.AddColumn<int>(
                name: "OrdemDeServicoId",
                table: "Servicos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_OrdemDeServicoId",
                table: "Servicos",
                column: "OrdemDeServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_OrdensDeServico_OrdemDeServicoId",
                table: "Servicos",
                column: "OrdemDeServicoId",
                principalTable: "OrdensDeServico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_OrdensDeServico_OrdemDeServicoId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_OrdemDeServicoId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "OrdemDeServicoId",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "CustoProdutoNoServico",
                table: "ServicoProdutos",
                newName: "PrecoAdicional");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Produtos",
                newName: "NomeProduto");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Produtos",
                newName: "DescricaoProduto");

            migrationBuilder.RenameColumn(
                name: "CustoVenda",
                table: "Produtos",
                newName: "CustoVendaCliente");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "Produtos",
                newName: "CategoriaProduto");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Servicos",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoAssociadoServico",
                table: "Produtos",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProduto",
                table: "OrdensDeServico",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "OrdensDeServico",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDeServico_ProdutoId",
                table: "OrdensDeServico",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoId",
                table: "OrdensDeServico",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class ajusteDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoIdProduto",
                table: "OrdensDeServico");

            migrationBuilder.RenameColumn(
                name: "QtdProduto",
                table: "Produtos",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "PrecoProduto",
                table: "Produtos",
                newName: "CustoVendaCliente");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "Produtos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProdutoIdProduto",
                table: "OrdensDeServico",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensDeServico_ProdutoIdProduto",
                table: "OrdensDeServico",
                newName: "IX_OrdensDeServico_ProdutoId");

            migrationBuilder.AddColumn<decimal>(
                name: "CustoAssociadoServico",
                table: "Produtos",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoInterno",
                table: "Produtos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoId",
                table: "OrdensDeServico",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoId",
                table: "OrdensDeServico");

            migrationBuilder.DropColumn(
                name: "CustoAssociadoServico",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CustoInterno",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Produtos",
                newName: "QtdProduto");

            migrationBuilder.RenameColumn(
                name: "CustoVendaCliente",
                table: "Produtos",
                newName: "PrecoProduto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Produtos",
                newName: "IdProduto");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "OrdensDeServico",
                newName: "ProdutoIdProduto");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensDeServico_ProdutoId",
                table: "OrdensDeServico",
                newName: "IX_OrdensDeServico_ProdutoIdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoIdProduto",
                table: "OrdensDeServico",
                column: "ProdutoIdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto");
        }
    }
}

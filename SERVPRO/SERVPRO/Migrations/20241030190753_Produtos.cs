using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class Produtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdProduto",
                table: "OrdensDeServico",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProdutoIdProduto",
                table: "OrdensDeServico",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<string>(type: "text", nullable: false),
                    NomeProduto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DescricaoProduto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoriaProduto = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    PrecoProduto = table.Column<string>(type: "text", nullable: false),
                    QtdProduto = table.Column<string>(type: "text", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDeServico_ProdutoIdProduto",
                table: "OrdensDeServico",
                column: "ProdutoIdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoIdProduto",
                table: "OrdensDeServico",
                column: "ProdutoIdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensDeServico_Produtos_ProdutoIdProduto",
                table: "OrdensDeServico");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_OrdensDeServico_ProdutoIdProduto",
                table: "OrdensDeServico");

            migrationBuilder.DropColumn(
                name: "IdProduto",
                table: "OrdensDeServico");

            migrationBuilder.DropColumn(
                name: "ProdutoIdProduto",
                table: "OrdensDeServico");
        }
    }
}

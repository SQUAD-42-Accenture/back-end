using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class AddServicoAndServicoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    OrdemDeServicoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicos_OrdensDeServico_OrdemDeServicoId",
                        column: x => x.OrdemDeServicoId,
                        principalTable: "OrdensDeServico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServicoProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServicoId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<string>(type: "text", nullable: false),
                    PrecoAdicional = table.Column<decimal>(type: "numeric", nullable: false),
                    OrdemDeServicoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicoProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicoProdutos_OrdensDeServico_OrdemDeServicoId",
                        column: x => x.OrdemDeServicoId,
                        principalTable: "OrdensDeServico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicoProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicoProdutos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicoProdutos_OrdemDeServicoId",
                table: "ServicoProdutos",
                column: "OrdemDeServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoProdutos_ProdutoId",
                table: "ServicoProdutos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoProdutos_ServicoId",
                table: "ServicoProdutos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_OrdemDeServicoId",
                table: "Servicos",
                column: "OrdemDeServicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicoProdutos");

            migrationBuilder.DropTable(
                name: "Servicos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionIdOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Se a chave estrangeira não existir, não tente removê-la
            // migrationBuilder.DropForeignKey(name: "FK_ServicoProdutos_OrdensDeServico_OrdemDeServicoId", table: "ServicoProdutos");

            migrationBuilder.AddColumn<int>(
                name: "OrdemDeServicoId",
                table: "ServicoProdutos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoProdutos_OrdensDeServico_OrdemDeServicoId",
                table: "ServicoProdutos",
                column: "OrdemDeServicoId",
                principalTable: "OrdensDeServico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoProdutos_OrdensDeServico_OrdemDeServicoId",
                table: "ServicoProdutos");

            migrationBuilder.AlterColumn<int>(
                name: "OrdemDeServicoId",
                table: "ServicoProdutos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoProdutos_OrdensDeServico_OrdemDeServicoId",
                table: "ServicoProdutos",
                column: "OrdemDeServicoId",
                principalTable: "OrdensDeServico",
                principalColumn: "Id");
        }
    }
}

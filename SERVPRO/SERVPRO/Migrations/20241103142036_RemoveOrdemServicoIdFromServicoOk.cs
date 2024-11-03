using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrdemServicoIdFromServicoOk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

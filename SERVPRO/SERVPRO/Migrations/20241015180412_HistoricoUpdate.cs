using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class HistoricoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosOS_OrdensDeServico_OrdemDeServicoId1",
                table: "HistoricosOS");

            migrationBuilder.DropIndex(
                name: "IX_HistoricosOS_OrdemDeServicoId1",
                table: "HistoricosOS");

            migrationBuilder.DropColumn(
                name: "OrdemDeServicoId1",
                table: "HistoricosOS");

            migrationBuilder.AlterColumn<int>(
                name: "OrdemDeServicoId",
                table: "HistoricosOS",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosOS_OrdemDeServicoId",
                table: "HistoricosOS",
                column: "OrdemDeServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosOS_OrdensDeServico_OrdemDeServicoId",
                table: "HistoricosOS",
                column: "OrdemDeServicoId",
                principalTable: "OrdensDeServico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosOS_OrdensDeServico_OrdemDeServicoId",
                table: "HistoricosOS");

            migrationBuilder.DropIndex(
                name: "IX_HistoricosOS_OrdemDeServicoId",
                table: "HistoricosOS");

            migrationBuilder.AlterColumn<string>(
                name: "OrdemDeServicoId",
                table: "HistoricosOS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdemDeServicoId1",
                table: "HistoricosOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosOS_OrdemDeServicoId1",
                table: "HistoricosOS",
                column: "OrdemDeServicoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosOS_OrdensDeServico_OrdemDeServicoId1",
                table: "HistoricosOS",
                column: "OrdemDeServicoId1",
                principalTable: "OrdensDeServico",
                principalColumn: "Id");
        }
    }
}

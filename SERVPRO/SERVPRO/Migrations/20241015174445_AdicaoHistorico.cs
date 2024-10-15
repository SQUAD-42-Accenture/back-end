using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoHistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricosOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdemDeServicoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdemDeServicoId1 = table.Column<int>(type: "int", nullable: true),
                    TecnicoCPF = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosOS_OrdensDeServico_OrdemDeServicoId1",
                        column: x => x.OrdemDeServicoId1,
                        principalTable: "OrdensDeServico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricosOS_Usuario_TecnicoCPF",
                        column: x => x.TecnicoCPF,
                        principalTable: "Usuario",
                        principalColumn: "CPF");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosOS_OrdemDeServicoId1",
                table: "HistoricosOS",
                column: "OrdemDeServicoId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosOS_TecnicoCPF",
                table: "HistoricosOS",
                column: "TecnicoCPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosOS");
        }
    }
}

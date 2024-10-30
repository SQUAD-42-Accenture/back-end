using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoCamposOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OrdensDeServico",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "MetodoPagamento",
                table: "OrdensDeServico",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValorTotal",
                table: "OrdensDeServico",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetodoPagamento",
                table: "OrdensDeServico");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "OrdensDeServico");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "OrdensDeServico",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

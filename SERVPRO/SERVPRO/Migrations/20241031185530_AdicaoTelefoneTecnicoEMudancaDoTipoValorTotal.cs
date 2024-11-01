using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoTelefoneTecnicoEMudancaDoTipoValorTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Tecnicos",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("ALTER TABLE \"OrdensDeServico\" ALTER COLUMN \"ValorTotal\" TYPE numeric(10,2) USING \"ValorTotal\"::numeric(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "OrdensDeServico",
                type: "numeric(10,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Tecnicos");

            migrationBuilder.AlterColumn<string>(
                name: "ValorTotal",
                table: "OrdensDeServico",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldNullable: true);
        }
    }
}

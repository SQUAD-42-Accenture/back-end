using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class fotoperfilvyte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPath",
                table: "Clientes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Clientes",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "FotoPath",
                table: "Clientes",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}

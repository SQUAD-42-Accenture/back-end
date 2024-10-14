using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteCPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteCPF);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Serial = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteCPF = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Serial);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Clientes_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Clientes",
                        principalColumn: "ClienteCPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ClienteCPF",
                table: "Equipamentos",
                column: "ClienteCPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}

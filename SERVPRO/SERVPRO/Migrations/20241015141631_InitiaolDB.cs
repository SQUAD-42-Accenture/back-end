using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class InitiaolDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Especialidade = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.CPF);
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
                        name: "FK_Equipamentos_Usuario_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Usuario",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdensDeServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ClienteCPF = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SerialEquipamento = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TecnicoCPF = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensDeServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdensDeServico_Equipamentos_SerialEquipamento",
                        column: x => x.SerialEquipamento,
                        principalTable: "Equipamentos",
                        principalColumn: "Serial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdensDeServico_Usuario_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Usuario",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdensDeServico_Usuario_TecnicoCPF",
                        column: x => x.TecnicoCPF,
                        principalTable: "Usuario",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ClienteCPF",
                table: "Equipamentos",
                column: "ClienteCPF");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDeServico_ClienteCPF",
                table: "OrdensDeServico",
                column: "ClienteCPF");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDeServico_SerialEquipamento",
                table: "OrdensDeServico",
                column: "SerialEquipamento");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDeServico_TecnicoCPF",
                table: "OrdensDeServico",
                column: "TecnicoCPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdensDeServico");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class TPTPostgreDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    TipoUsuario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Endereco = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CPF);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_CPF",
                        column: x => x.CPF,
                        principalTable: "Usuarios",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Especialidade = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.CPF);
                    table.ForeignKey(
                        name: "FK_Tecnicos_Usuarios_CPF",
                        column: x => x.CPF,
                        principalTable: "Usuarios",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Serial = table.Column<string>(type: "text", nullable: false),
                    Marca = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClienteCPF = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Serial);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Clientes_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Clientes",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdensDeServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dataAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dataConclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ClienteCPF = table.Column<string>(type: "text", nullable: true),
                    SerialEquipamento = table.Column<string>(type: "text", nullable: true),
                    TecnicoCPF = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensDeServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdensDeServico_Clientes_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Clientes",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdensDeServico_Equipamentos_SerialEquipamento",
                        column: x => x.SerialEquipamento,
                        principalTable: "Equipamentos",
                        principalColumn: "Serial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdensDeServico_Tecnicos_TecnicoCPF",
                        column: x => x.TecnicoCPF,
                        principalTable: "Tecnicos",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 255, nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    OrdemDeServicoId = table.Column<int>(type: "integer", nullable: true),
                    TecnicoCPF = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosOS_OrdensDeServico_OrdemDeServicoId",
                        column: x => x.OrdemDeServicoId,
                        principalTable: "OrdensDeServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosOS_Tecnicos_TecnicoCPF",
                        column: x => x.TecnicoCPF,
                        principalTable: "Tecnicos",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ClienteCPF",
                table: "Equipamentos",
                column: "ClienteCPF");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosOS_OrdemDeServicoId",
                table: "HistoricosOS",
                column: "OrdemDeServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosOS_TecnicoCPF",
                table: "HistoricosOS",
                column: "TecnicoCPF");

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
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "HistoricosOS");

            migrationBuilder.DropTable(
                name: "OrdensDeServico");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}

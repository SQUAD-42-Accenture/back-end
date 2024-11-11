using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SERVPRO.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoColunaIdOSemServios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verificar e remover a constraint se ela existir
            migrationBuilder.Sql(@"DO $$ 
    BEGIN
        IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_Servicos_OrdensDeServico_OrdemDeServicoId') THEN
            ALTER TABLE ""Servicos"" DROP CONSTRAINT ""FK_Servicos_OrdensDeServico_OrdemDeServicoId"";
        END IF;
    END $$;");

            // Verificar e remover o índice se ele existir
            migrationBuilder.Sql(@"DO $$ 
    BEGIN
        IF EXISTS (SELECT 1 FROM pg_indexes WHERE indexname = 'IX_Servicos_OrdemDeServicoId') THEN
            DROP INDEX ""IX_Servicos_OrdemDeServicoId"";
        END IF;
    END $$;");

            // Verificar se a coluna OrdemDeServicoId existe antes de tentar removê-la
            migrationBuilder.Sql(@"DO $$ 
    BEGIN
        IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'Servicos' AND column_name = 'OrdemDeServicoId') THEN
            ALTER TABLE ""Servicos"" DROP COLUMN ""OrdemDeServicoId"";
        END IF;
    END $$;");

            // Alterar as colunas na tabela Produtos
            migrationBuilder.AlterColumn<decimal>(
                name: "CustoVenda",
                table: "Produtos",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoInterno",
                table: "Produtos",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdemDeServicoId",
                table: "Servicos",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoVenda",
                table: "Produtos",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoInterno",
                table: "Produtos",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

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

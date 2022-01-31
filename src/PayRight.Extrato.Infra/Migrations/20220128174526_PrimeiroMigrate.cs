using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRight.Extrato.Infra.Migrations
{
    public partial class PrimeiroMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "extratos_contas_corrente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContaCorrenteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "NOW()"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    mes = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    ano = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false, defaultValue: 0m),
                    TotalEstimado = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_extratos_contas_corrente", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "atividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExtratoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    NomeAtividade_Nome = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeAtividade_Descricao = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false, defaultValue: 0m),
                    TipoAtividade = table.Column<int>(type: "int", nullable: false),
                    Pago = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_atividades_extratos_contas_corrente_ExtratoId",
                        column: x => x.ExtratoId,
                        principalTable: "extratos_contas_corrente",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_atividades_ExtratoId",
                table: "atividades",
                column: "ExtratoId");

            migrationBuilder.CreateIndex(
                name: "IX_atividades_Id",
                table: "atividades",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "idx_conta_corrente_id",
                table: "extratos_contas_corrente",
                column: "ContaCorrenteId");

            migrationBuilder.CreateIndex(
                name: "idx_usuario_id",
                table: "extratos_contas_corrente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_extratos_contas_corrente_Id",
                table: "extratos_contas_corrente",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividades");

            migrationBuilder.DropTable(
                name: "extratos_contas_corrente");
        }
    }
}

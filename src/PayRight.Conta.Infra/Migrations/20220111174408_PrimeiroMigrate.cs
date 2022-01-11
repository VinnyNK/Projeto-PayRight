using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRight.Conta.Infra.Migrations
{
    public partial class PrimeiroMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contas_correntes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Saldo = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false, defaultValue: 0m),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "NOW()"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    nome = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apelido = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ultima_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contas_correntes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "idx_usuario_id",
                table: "contas_correntes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_contas_correntes_Id",
                table: "contas_correntes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contas_correntes");
        }
    }
}

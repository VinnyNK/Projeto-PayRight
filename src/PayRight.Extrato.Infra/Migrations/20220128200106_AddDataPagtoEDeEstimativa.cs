using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRight.Extrato.Infra.Migrations
{
    public partial class AddDataPagtoEDeEstimativa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPagamento",
                table: "atividades",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EstimativaPagamento",
                table: "atividades",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "atividades");

            migrationBuilder.DropColumn(
                name: "EstimativaPagamento",
                table: "atividades");
        }
    }
}

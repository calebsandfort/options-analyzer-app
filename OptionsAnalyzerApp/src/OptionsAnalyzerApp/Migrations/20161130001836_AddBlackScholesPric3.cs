using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionsAnalyzerApp.Migrations
{
    public partial class AddBlackScholesPric3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BlackScholesPL",
                table: "Options",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BlackScholesPLPercent",
                table: "Options",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BlackScholesPriceChange",
                table: "Options",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackScholesPL",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "BlackScholesPLPercent",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "BlackScholesPriceChange",
                table: "Options");
        }
    }
}

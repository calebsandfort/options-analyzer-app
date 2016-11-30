using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionsAnalyzerApp.Migrations
{
    public partial class AddRiskFreeInterestRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RickFreeInterestRate",
                table: "TradingAccounts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RickFreeInterestRate",
                table: "TradingAccounts");
        }
    }
}

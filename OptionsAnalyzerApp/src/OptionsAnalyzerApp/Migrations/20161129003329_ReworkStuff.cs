using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OptionsAnalyzerApp.Migrations
{
    public partial class ReworkStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionPriceChangeTargets");

            migrationBuilder.DropTable(
                name: "TradingAccountPriceChangeTargets");

            migrationBuilder.AddColumn<decimal>(
                name: "ExpectedPriceChange",
                table: "TradingAccounts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeltaPL",
                table: "Options",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeltaPLPercent",
                table: "Options",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedPriceChange",
                table: "TradingAccounts");

            migrationBuilder.DropColumn(
                name: "DeltaPL",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "DeltaPLPercent",
                table: "Options");

            migrationBuilder.CreateTable(
                name: "OptionPriceChangeTargets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionID = table.Column<int>(nullable: false),
                    PL = table.Column<decimal>(nullable: false),
                    PLPercent = table.Column<decimal>(nullable: false),
                    PriceChangeTarget = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionPriceChangeTargets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OptionPriceChangeTargets_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradingAccountPriceChangeTargets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PriceChangeTarget = table.Column<decimal>(nullable: false),
                    TradingAccountID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingAccountPriceChangeTargets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradingAccountPriceChangeTargets_TradingAccounts_TradingAccountID",
                        column: x => x.TradingAccountID,
                        principalTable: "TradingAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionPriceChangeTargets_OptionID",
                table: "OptionPriceChangeTargets",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TradingAccountPriceChangeTargets_TradingAccountID",
                table: "TradingAccountPriceChangeTargets",
                column: "TradingAccountID");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OptionsAnalyzerApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ask = table.Column<decimal>(nullable: false),
                    Bid = table.Column<decimal>(nullable: false),
                    Delta = table.Column<decimal>(nullable: false),
                    OptionID = table.Column<int>(nullable: true),
                    OptionType = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Strike = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Options_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradingAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    UnitSize = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingAccounts", x => x.ID);
                });

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
                name: "IX_Options_OptionID",
                table: "Options",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_OptionPriceChangeTargets_OptionID",
                table: "OptionPriceChangeTargets",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TradingAccountPriceChangeTargets_TradingAccountID",
                table: "TradingAccountPriceChangeTargets",
                column: "TradingAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionPriceChangeTargets");

            migrationBuilder.DropTable(
                name: "TradingAccountPriceChangeTargets");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "TradingAccounts");
        }
    }
}

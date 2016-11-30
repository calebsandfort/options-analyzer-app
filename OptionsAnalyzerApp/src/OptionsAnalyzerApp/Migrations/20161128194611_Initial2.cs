using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionsAnalyzerApp.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Options_OptionID",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionID",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "OptionID",
                table: "Options");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionID",
                table: "Options",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionID",
                table: "Options",
                column: "OptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Options_OptionID",
                table: "Options",
                column: "OptionID",
                principalTable: "Options",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Shop.DAL.Core.Migrations
{
    public partial class AddCountInfoToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products", 
                keyColumn: "Name", 
                keyValue: "Clash Royale", 
                column: "Count", 
                value: 10
                );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Fallout 4",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "FIFA 20",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "FlappyBird",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Fortnit",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "GTA: San Andreas",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Hades",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Half-Life 2",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Mario Kart 8",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Minecraft",
                column: "Count",
                value: 10
                );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "PokemonGo",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Spider-Man",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "Subway Surfers",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "The Sims 2",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "The Sims 3",
                column: "Count",
                value: 10
            );

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Name",
                keyValue: "The Witcher 3: Wild Hunt",
                column: "Count",
                value: 10
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

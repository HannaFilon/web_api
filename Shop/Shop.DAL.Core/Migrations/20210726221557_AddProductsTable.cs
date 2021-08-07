using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.DAL.Core.Migrations
{
    public partial class AddProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "Date", nullable: false),
                    TotalRating = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateCreated",
                table: "Products",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Platform",
                table: "Products",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TotalRating",
                table: "Products",
                column: "TotalRating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

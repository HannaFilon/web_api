using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.DAL.Core.Migrations
{
    public partial class AddProductRatingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Products",
                newName: "AgeRating");

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProductRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AgeRating",
                table: "Products",
                column: "AgeRating");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Genre",
                table: "Products",
                column: "Genre");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Price",
                table: "Products",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId",
                table: "ProductRatings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_UserId",
                table: "ProductRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_Products_AgeRating",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Genre",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Price",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "AgeRating",
                table: "Products",
                newName: "Rating");

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}

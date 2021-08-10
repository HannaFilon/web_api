using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.DAL.Core.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "Id", "Name", "Platform", "DateCreated" },
              values: new object[]
              {
                    Guid.NewGuid(),
                    "GTA: San Andreas",
                    0,
                    DateTime.Parse("26.10.2004")
              });

            migrationBuilder.InsertData(
               table: "Products",
               columns: new[] { "Id", "Name", "Platform", "DateCreated"},
               values: new object[]
               {
                    Guid.NewGuid(),
                    "The Sims 3",
                    2,
                    DateTime.Parse("26.10.2010")
               });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Mario Kart 8",
                    4,
                    DateTime.Parse("29.05.2014")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Hades",
                    4,
                    DateTime.Parse("6.12.2018")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Minecraft",
                    0,
                    DateTime.Parse("18.11.2011")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Fallout 4",
                    1,
                    DateTime.Parse("10.11.2015")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Minecraft",
                    3,
                    DateTime.Parse("16.08.2011")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "The Sims 2",
                    0,
                    DateTime.Parse("14.09.2004")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated"},
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Half-Life 2",
                    0,
                    DateTime.Parse("16.11.2010")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Clash Royale",
                    3,
                    DateTime.Parse("02.03.2016")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated", "TotalRating" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "The Witcher 3: Wild Hunt",
                    2,
                    DateTime.Parse("18.05.2015"),
                    4.65
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "PokemonGo",
                    3,
                    DateTime.Parse("06.07.2016")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Spider-Man",
                    2,
                    DateTime.Parse("07.09.2018")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated"},
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Fortnit",
                    2,
                    DateTime.Parse("21.07.2017")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Subway Surfers",
                    3,
                    DateTime.Parse("23.05.2012")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "FlappyBird",
                    3,
                    DateTime.Parse("24.05.2013")
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Platform", "DateCreated" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "FIFA 20",
                    1,
                    DateTime.Parse("27.09.2019")
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

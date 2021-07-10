using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Shop.DAL.Core.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Admin",
                    "ADMIN",
                    Guid.NewGuid().ToString()
                    });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "User",
                    "USER",
                    Guid.NewGuid().ToString()
                    });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

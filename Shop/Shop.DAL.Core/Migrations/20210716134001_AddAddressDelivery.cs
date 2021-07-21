using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.DAL.Core.Migrations
{
    public partial class AddAddressDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressDelivery",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressDelivery",
                table: "AspNetUsers");
        }
    }
}

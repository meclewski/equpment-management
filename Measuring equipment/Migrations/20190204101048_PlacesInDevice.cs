using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class PlacesInDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Devices",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Devices");
        }
    }
}

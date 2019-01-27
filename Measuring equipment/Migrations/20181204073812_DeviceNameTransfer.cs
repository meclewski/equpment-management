using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class DeviceNameTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "Types",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "Types");

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "Devices",
                nullable: true);
        }
    }
}

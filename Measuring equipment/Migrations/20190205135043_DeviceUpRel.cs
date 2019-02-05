using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class DeviceUpRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Places_PlaceId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Types_TypeId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Places_PlaceId",
                table: "Devices",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Types_TypeId",
                table: "Devices",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Places_PlaceId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Types_TypeId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Places_PlaceId",
                table: "Devices",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Types_TypeId",
                table: "Devices",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

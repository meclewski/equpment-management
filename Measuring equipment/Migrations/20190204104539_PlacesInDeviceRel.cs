using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class PlacesInDeviceRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Devices_PlaceId",
                table: "Devices",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Places_PlaceId",
                table: "Devices",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Places_PlaceId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_PlaceId",
                table: "Devices");
        }
    }
}

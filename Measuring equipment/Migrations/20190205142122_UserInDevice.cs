using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class UserInDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Devices");
        }
    }
}

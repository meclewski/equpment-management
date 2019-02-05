using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class PlacesDeptsRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Places_DepartmentId",
                table: "Places",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Departments_DepartmentId",
                table: "Places",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Departments_DepartmentId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_DepartmentId",
                table: "Places");
        }
    }
}

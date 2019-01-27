using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class TypeLabRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Laboratories_LaboratoriesLaboratoryId",
                table: "Types");

            migrationBuilder.RenameColumn(
                name: "LaboratoriesLaboratoryId",
                table: "Types",
                newName: "LaboratoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Types_LaboratoriesLaboratoryId",
                table: "Types",
                newName: "IX_Types_LaboratoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types");

            migrationBuilder.RenameColumn(
                name: "LaboratoryId",
                table: "Types",
                newName: "LaboratoriesLaboratoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Types_LaboratoryId",
                table: "Types",
                newName: "IX_Types_LaboratoriesLaboratoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Laboratories_LaboratoriesLaboratoryId",
                table: "Types",
                column: "LaboratoriesLaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

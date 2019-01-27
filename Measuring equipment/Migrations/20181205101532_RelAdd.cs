using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class RelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationId",
                table: "Types",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryId",
                table: "Types",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types",
                column: "VerificationId",
                principalTable: "Verification",
                principalColumn: "VerificationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationId",
                table: "Types",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryId",
                table: "Types",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types",
                column: "VerificationId",
                principalTable: "Verification",
                principalColumn: "VerificationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

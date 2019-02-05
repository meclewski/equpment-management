using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class RestUpRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Departments_DepartmentId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Producers_ProducerId",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Departments_DepartmentId",
                table: "Places",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Producers_ProducerId",
                table: "Types",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types",
                column: "VerificationId",
                principalTable: "Verification",
                principalColumn: "VerificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Departments_DepartmentId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Producers_ProducerId",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Departments_DepartmentId",
                table: "Places",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Laboratories_LaboratoryId",
                table: "Types",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Producers_ProducerId",
                table: "Types",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Verification_VerificationId",
                table: "Types",
                column: "VerificationId",
                principalTable: "Verification",
                principalColumn: "VerificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

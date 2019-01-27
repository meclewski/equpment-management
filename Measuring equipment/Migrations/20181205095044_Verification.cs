using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Measuring_equipment.Migrations
{
    public partial class Verification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "Types",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    VerificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VerificationName = table.Column<string>(nullable: true),
                    VerificationDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.VerificationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Types_VerificationId",
                table: "Types",
                column: "VerificationId");

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
                name: "FK_Types_Verification_VerificationId",
                table: "Types");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Types_VerificationId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "Types");
        }
    }
}

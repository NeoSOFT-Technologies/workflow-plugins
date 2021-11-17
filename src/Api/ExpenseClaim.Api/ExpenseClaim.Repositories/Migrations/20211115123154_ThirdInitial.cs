using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseClaim.Repositories.Migrations
{
    public partial class ThirdInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimType_ExpenseClaims_ClaimId",
                table: "ClaimType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimType",
                table: "ClaimType");

            migrationBuilder.RenameTable(
                name: "ClaimType",
                newName: "ClaimTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimType_ClaimId",
                table: "ClaimTypes",
                newName: "IX_ClaimTypes_ClaimId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSanctioned",
                table: "ClaimTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimTypes",
                table: "ClaimTypes",
                column: "ClaimTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimTypes_ExpenseClaims_ClaimId",
                table: "ClaimTypes",
                column: "ClaimId",
                principalTable: "ExpenseClaims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimTypes_ExpenseClaims_ClaimId",
                table: "ClaimTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimTypes",
                table: "ClaimTypes");

            migrationBuilder.DropColumn(
                name: "IsSanctioned",
                table: "ClaimTypes");

            migrationBuilder.RenameTable(
                name: "ClaimTypes",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimTypes_ClaimId",
                table: "ClaimType",
                newName: "IX_ClaimType_ClaimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimType",
                table: "ClaimType",
                column: "ClaimTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimType_ExpenseClaims_ClaimId",
                table: "ClaimType",
                column: "ClaimId",
                principalTable: "ExpenseClaims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseClaim.Repositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseClaims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalClaimAmount = table.Column<decimal>(nullable: false),
                    SanctionedAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseClaims", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "ClaimType",
                columns: table => new
                {
                    ClaimTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimTypeName = table.Column<string>(nullable: true),
                    ClaimAmount = table.Column<decimal>(nullable: false),
                    ClaimTypeDescription = table.Column<string>(nullable: true),
                    ClaimId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimType", x => x.ClaimTypeId);
                    table.ForeignKey(
                        name: "FK_ClaimType_ExpenseClaims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "ExpenseClaims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimType_ClaimId",
                table: "ClaimType",
                column: "ClaimId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimType");

            migrationBuilder.DropTable(
                name: "ExpenseClaims");
        }
    }
}

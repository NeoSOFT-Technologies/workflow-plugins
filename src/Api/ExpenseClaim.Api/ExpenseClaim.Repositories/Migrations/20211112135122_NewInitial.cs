using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseClaim.Repositories.Migrations
{
    public partial class NewInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSanctioned",
                table: "ExpenseClaims",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSanctioned",
                table: "ExpenseClaims");
        }
    }
}

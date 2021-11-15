using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HireEmployee.Repositories.Migrations
{
    public partial class AddedDesignationcoloumntoCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("5606e734-8d48-49af-a981-4ad9d862cc8d"),
                column: "Designation",
                value: "Software Engineer");

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("5b1c5587-eddd-4fc0-a6ca-3f83983b14e5"),
                column: "Designation",
                value: "Software Engineer");

            migrationBuilder.UpdateData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("fc5c071a-89a4-4622-a7ea-7afcb08ed22e"),
                column: "Designation",
                value: "Software Engineer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Candidates");
        }
    }
}

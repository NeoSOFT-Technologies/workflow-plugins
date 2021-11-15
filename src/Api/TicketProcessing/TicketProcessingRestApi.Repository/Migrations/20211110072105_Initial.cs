using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketProcessingRestApi.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    assignManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    assignPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    assignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isResolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Attachment", "CreateDate", "Discription", "Priority", "TicketType", "assignDate", "assignManager", "assignPerson", "createdBy", "isResolved" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "query.pdf", new DateTime(2021, 11, 10, 12, 51, 4, 626, DateTimeKind.Local).AddTicks(37), "new query", "High", "IT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "neha.haridas@gmail.com", "vinay@xyz.com", "Anshul@gmail.com", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}

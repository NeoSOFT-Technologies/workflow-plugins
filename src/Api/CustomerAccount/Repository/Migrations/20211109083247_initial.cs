using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersDetail",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetail", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "UsersDetail",
                columns: new[] { "UserId", "AccountNumber", "DocumentName", "Email", "MobileNumber", "Name" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "721702010010565", "Adhar Card", "vivek@xyz.com", "7754951743", "Vivek" });

            migrationBuilder.InsertData(
                table: "UsersDetail",
                columns: new[] { "UserId", "AccountNumber", "DocumentName", "Email", "MobileNumber", "Name" },
                values: new object[] { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "721702010010566", "Adhar Card", "amisha@xyz.com", "7754951745", "Amisha" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDetail");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceCustomerApi.Repositories.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberofResource = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsInvoicePaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "ClientName", "Email", "Phone" },
                values: new object[] { new Guid("1213179a-7837-473a-a4d5-a5571b43e6a6"), "Ankit", "ankit@gmail.com", "9873892812" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "ClientName", "Email", "Phone" },
                values: new object[] { new Guid("2413179a-7837-493a-a4d5-a5571b43e6a6"), "Neha", "neha@gmail.com", "9173292812" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "Amount", "ClientId", "Date", "IsInvoicePaid", "NumberofResource" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), 2000m, new Guid("1213179a-7837-473a-a4d5-a5571b43e6a6"), new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), false, 10 });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "Amount", "ClientId", "Date", "IsInvoicePaid", "NumberofResource" },
                values: new object[] { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), 3000m, new Guid("2413179a-7837-493a-a4d5-a5571b43e6a6"), new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), false, 9 });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

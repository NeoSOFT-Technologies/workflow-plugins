using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HireEmployee.Repositories.Migrations
{
    public partial class Addedtablesincludingbooleanfieldswithintialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Skill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedSalary = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<bool>(type: "bit", nullable: false),
                    PhoneScreening = table.Column<bool>(type: "bit", nullable: false),
                    Interview = table.Column<bool>(type: "bit", nullable: false),
                    OfferAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Designation", "Experience", "Salary", "Skills" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Software Engineer", 3, "30,000 - 40,0000", "Dot Net, NodeJS, SQL" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Designation", "Experience", "Salary", "Skills" },
                values: new object[] { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Automation Tester", 1, "25,000 - 28,0000", "Dot Net, Django, NodeJS, SQL" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Age", "Email", "ExpectedSalary", "Experience", "Interview", "JobId", "Name", "OfferAccepted", "PhoneScreening", "Resume", "Review", "Skill" },
                values: new object[] { new Guid("5b1c5587-eddd-4fc0-a6ca-3f83983b14e5"), 21, "shivam@gmail.com", 380000, 3, false, new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Shivam Gupta", false, false, "ShivamResume.docx", false, "Dot Net, Python, Django, ReactJs" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Age", "Email", "ExpectedSalary", "Experience", "Interview", "JobId", "Name", "OfferAccepted", "PhoneScreening", "Resume", "Review", "Skill" },
                values: new object[] { new Guid("5606e734-8d48-49af-a981-4ad9d862cc8d"), 20, "alista@gmail.com", 300000, 1, false, new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Alista Menezes", false, false, "AlistaResume.docx", false, "Python, Angular, NodeJS " });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Age", "Email", "ExpectedSalary", "Experience", "Interview", "JobId", "Name", "OfferAccepted", "PhoneScreening", "Resume", "Review", "Skill" },
                values: new object[] { new Guid("fc5c071a-89a4-4622-a7ea-7afcb08ed22e"), 35, "rakesh@gmail.com", 410000, 7, false, new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Rakesh Verma", false, false, "RakeshResume.docx", false, "Java, Dot Net, MongoDb" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobId",
                table: "Candidates",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}

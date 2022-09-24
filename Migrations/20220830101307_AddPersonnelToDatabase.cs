using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnalTrackingProject.Migrations
{
    public partial class AddPersonnelToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Personals",
                columns: table => new
                {
                    PersonnelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonnelLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonnelAge = table.Column<byte>(type: "tinyint", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personals", x => x.PersonnelID);
                    table.ForeignKey(
                        name: "FK_Personals_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inouttimes",
                columns: table => new
                {
                    InOutTimeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonnelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inouttimes", x => x.InOutTimeID);
                    table.ForeignKey(
                        name: "FK_Inouttimes_Personals_PersonnelID",
                        column: x => x.PersonnelID,
                        principalTable: "Personals",
                        principalColumn: "PersonnelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inouttimes_PersonnelID",
                table: "Inouttimes",
                column: "PersonnelID");

            migrationBuilder.CreateIndex(
                name: "IX_Personals_DepartmentID",
                table: "Personals",
                column: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inouttimes");

            migrationBuilder.DropTable(
                name: "Personals");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

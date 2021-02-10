using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngressYear = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngressYear = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Professor",
                columns: new[] { "Id", "DateOfBirth", "IngressYear", "ProfessorName" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 10, 10, 40, 12, 993, DateTimeKind.Local).AddTicks(5538), new DateTime(2021, 2, 10, 10, 40, 12, 993, DateTimeKind.Local).AddTicks(5045), "Helena" },
                    { 2, new DateTime(2021, 2, 10, 10, 40, 12, 993, DateTimeKind.Local).AddTicks(5974), new DateTime(2021, 2, 10, 10, 40, 12, 993, DateTimeKind.Local).AddTicks(5971), "Matilde" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "DateOfBirth", "IngressYear", "StudentName" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), "Mario Laiala" },
                    { 2, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), "Cirilo" },
                    { 3, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), "Maria Joaquina" }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "Name", "ProfessorId", "Room", "Schedule" },
                values: new object[] { 1, "Calculo 3", 2, null, new DateTime(2021, 2, 10, 10, 40, 12, 993, DateTimeKind.Local).AddTicks(7466) });

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProfessorId",
                table: "Course",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                table: "CourseStudent",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}

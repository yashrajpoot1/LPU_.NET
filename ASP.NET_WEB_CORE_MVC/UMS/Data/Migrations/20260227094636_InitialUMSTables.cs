using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialUMSTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseCode", "CourseName", "Credits", "Description", "Instructor" },
                values: new object[,]
                {
                    { 1, "CS101", "Introduction to Programming", 3, "Learn the basics of programming", "Dr. Anderson" },
                    { 2, "CS201", "Data Structures", 4, "Advanced data structures and algorithms", "Prof. Williams" },
                    { 3, "MATH101", "Calculus I", 4, "Introduction to calculus", "Dr. Brown" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "EnrollmentDate", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@university.edu", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe", "555-0101" },
                    { 2, new DateTime(2001, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@university.edu", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Smith", "555-0102" },
                    { 3, new DateTime(1999, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike.johnson@university.edu", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mike", "Johnson", "555-0103" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrollmentDate", "Grade", "Status", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "Completed", 1 },
                    { 2, 2, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active", 1 },
                    { 3, 1, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "B+", "Completed", 2 },
                    { 4, 3, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active", 2 },
                    { 5, 2, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "A-", "Completed", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}

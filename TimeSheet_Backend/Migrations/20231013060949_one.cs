using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobileno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId");
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    TimeSheetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    task = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hours = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.TimeSheetId);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "ActivityId", "ActivityName", "ProjectId" },
                values: new object[,]
                {
                    { 1, "Unit Testing", 0 },
                    { 2, "Acceptance Testing", 0 },
                    { 3, "Warranty/MC", 0 },
                    { 4, "System Testing", 0 },
                    { 5, "Coding/Implementation", 0 },
                    { 6, "Design", 0 },
                    { 7, "Support", 0 },
                    { 8, "Integration Testing", 0 },
                    { 9, "Requirements Development", 0 },
                    { 10, "Planning", 0 },
                    { 11, "PTO", 0 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "ActivityId", "ProjectName" },
                values: new object[,]
                {
                    { 1, null, "Persona Nutrition" },
                    { 2, null, "Puritains" },
                    { 3, null, "Nestle Health Sciences" },
                    { 4, null, "Market Central" },
                    { 5, null, "Family Central" },
                    { 6, null, "Internal POC" },
                    { 7, null, "External POC" },
                    { 8, null, "Marketing & Sales" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "roleId", "roleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Hr" },
                    { 3, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ActivityId",
                table: "Projects",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_ActivityId",
                table: "TimeSheets",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_ProjectId",
                table: "TimeSheets",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_UserId",
                table: "TimeSheets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class new09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 11,
                column: "ProjectId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 4,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 5,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 6,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 7,
                column: "ActivityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 8,
                column: "ActivityId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ActivityId",
                table: "Projects",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Activities_ActivityId",
                table: "Projects",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Activities_ActivityId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ActivityId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Activities");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class sdo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "roleId",
                keyValue: 2,
                column: "roleName",
                value: "Hr");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "roleId",
                keyValue: 3,
                column: "roleName",
                value: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "roleId",
                keyValue: 2,
                column: "roleName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "roleId",
                keyValue: 3,
                column: "roleName",
                value: "Hr");
        }
    }
}

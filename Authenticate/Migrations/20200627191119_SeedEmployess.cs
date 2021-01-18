using Microsoft.EntityFrameworkCore.Migrations;

namespace Authenticate.Migrations
{
    public partial class SeedEmployess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 1, 1, "Mark@gmail.com", "Mark" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

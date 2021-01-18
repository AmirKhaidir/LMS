using Microsoft.EntityFrameworkCore.Migrations;

namespace Authenticate.Migrations
{
    public partial class AddPhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Employee",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 2, 2, "Diego@gmail.com", "Diegp", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Employee");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authenticate.Migrations.ApplyOnBehalf
{
    public partial class ApplyOnBehalf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyBehalf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(nullable: true),
                    DateApply = table.Column<DateTime>(nullable: false),
                    Session = table.Column<string>(nullable: true),
                    LeaveType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyBehalf", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyBehalf");
        }
    }
}

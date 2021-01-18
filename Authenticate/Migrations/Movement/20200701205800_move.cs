using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authenticate.Migrations.Movement
{
    public partial class move : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    TimeIn = table.Column<DateTime>(nullable: false),
                    TimeOut = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movement");
        }
    }
}

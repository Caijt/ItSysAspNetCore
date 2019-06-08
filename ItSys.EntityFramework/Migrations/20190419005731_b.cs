using Microsoft.EntityFrameworkCore.Migrations;

namespace ItSys.EntityFramework.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_url",
                table: "SysUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_url",
                table: "SysUsers");
        }
    }
}

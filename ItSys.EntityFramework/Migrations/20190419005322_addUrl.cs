using Microsoft.EntityFrameworkCore.Migrations;

namespace ItSys.EntityFramework.Migrations
{
    public partial class addUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SysUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "SysUsers");
        }
    }
}

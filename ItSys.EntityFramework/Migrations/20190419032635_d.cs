using Microsoft.EntityFrameworkCore.Migrations;

namespace ItSys.EntityFramework.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "SysUsers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "SysUsers",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

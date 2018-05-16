using Microsoft.EntityFrameworkCore.Migrations;

namespace EF.Migrations
{
    public partial class X2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status_Value",
                table: "Packages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status_Value",
                table: "Packages",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

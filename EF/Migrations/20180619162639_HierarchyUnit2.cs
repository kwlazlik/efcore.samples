using Microsoft.EntityFrameworkCore.Migrations;

namespace EFC.Migrations
{
    public partial class HierarchyUnit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "HierarchyUnit",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "HierarchyUnit",
                newName: "name");
        }
    }
}

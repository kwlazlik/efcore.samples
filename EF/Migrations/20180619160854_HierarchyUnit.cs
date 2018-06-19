using Microsoft.EntityFrameworkCore.Migrations;

namespace EFC.Migrations
{
    public partial class HierarchyUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Packages");

            migrationBuilder.AddColumn<int>(
                name: "HierarchyUnitId",
                table: "Packages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HierarchyUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyUnit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_HierarchyUnitId",
                table: "Packages",
                column: "HierarchyUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_HierarchyUnit_HierarchyUnitId",
                table: "Packages",
                column: "HierarchyUnitId",
                principalTable: "HierarchyUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_HierarchyUnit_HierarchyUnitId",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "HierarchyUnit");

            migrationBuilder.DropIndex(
                name: "IX_Packages_HierarchyUnitId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "HierarchyUnitId",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Packages",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EFC.Migrations
{
    public partial class Seq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "testsequence");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "testsequence");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EFC.Migrations
{
    public partial class ExamIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier_Value",
                table: "Exams",
                nullable: true,
                defaultValueSql: "'Ident #' + CAST(NEXT VALUE FOR testsequence AS varchar(max))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier_Value",
                table: "Exams");
        }
    }
}

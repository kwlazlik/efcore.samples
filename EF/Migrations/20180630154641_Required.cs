using Microsoft.EntityFrameworkCore.Migrations;

namespace EFC.Migrations
{
    public partial class Required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamDifficulty_DifficultyKey",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExams_Grade_GradeKey",
                table: "StudentExams");

            migrationBuilder.AlterColumn<string>(
                name: "GradeKey",
                table: "StudentExams",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DifficultyKey",
                table: "Exams",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamDifficulty_DifficultyKey",
                table: "Exams",
                column: "DifficultyKey",
                principalTable: "ExamDifficulty",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExams_Grade_GradeKey",
                table: "StudentExams",
                column: "GradeKey",
                principalTable: "Grade",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamDifficulty_DifficultyKey",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExams_Grade_GradeKey",
                table: "StudentExams");

            migrationBuilder.AlterColumn<string>(
                name: "GradeKey",
                table: "StudentExams",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DifficultyKey",
                table: "Exams",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamDifficulty_DifficultyKey",
                table: "Exams",
                column: "DifficultyKey",
                principalTable: "ExamDifficulty",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExams_Grade_GradeKey",
                table: "StudentExams",
                column: "GradeKey",
                principalTable: "Grade",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

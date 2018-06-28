using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFC.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamDifficulty",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 16, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDifficulty", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 16, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbCreatedAt = table.Column<DateTime>(nullable: false),
                    DbCreatedBy = table.Column<string>(nullable: true),
                    DbModifiedAt = table.Column<DateTime>(nullable: false),
                    DbModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbCreatedAt = table.Column<DateTime>(nullable: false),
                    DbCreatedBy = table.Column<string>(nullable: true),
                    DbModifiedAt = table.Column<DateTime>(nullable: false),
                    DbModifiedBy = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SchoolClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_SchoolClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbCreatedAt = table.Column<DateTime>(nullable: false),
                    DbCreatedBy = table.Column<string>(nullable: true),
                    DbModifiedAt = table.Column<DateTime>(nullable: false),
                    DbModifiedBy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    DifficultyKey = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true),
                    Time = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_ExamDifficulty_DifficultyKey",
                        column: x => x.DifficultyKey,
                        principalTable: "ExamDifficulty",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbCreatedAt = table.Column<DateTime>(nullable: false),
                    DbCreatedBy = table.Column<string>(nullable: true),
                    DbModifiedAt = table.Column<DateTime>(nullable: false),
                    DbModifiedBy = table.Column<string>(nullable: true),
                    GradeKey = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    ExamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExams_Grade_GradeKey",
                        column: x => x.GradeKey,
                        principalTable: "Grade",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExams_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ExamDifficulty",
                columns: new[] { "Key", "Order", "Value" },
                values: new object[,]
                {
                    { "Easy", 1, "latwy" },
                    { "Medium", 2, "sredni" },
                    { "Hard", 3, "trudny" }
                });

            migrationBuilder.InsertData(
                table: "Grade",
                columns: new[] { "Key", "Discriminator", "Order", "Value" },
                values: new object[,]
                {
                    { "A", "m", 1, "bardzodobry" },
                    { "B", "m", 2, "dobry" },
                    { "C", "m", 3, "dostateczny" },
                    { "D", "m", 4, "dopuszczający" },
                    { "E", "m", 5, "niedostateczny" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_DifficultyKey",
                table: "Exams",
                column: "DifficultyKey");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_ExamId",
                table: "StudentExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_GradeKey",
                table: "StudentExams",
                column: "GradeKey");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_StudentId",
                table: "StudentExams",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolClassId",
                table: "Students",
                column: "SchoolClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExams");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ExamDifficulty");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "SchoolClasses");
        }
    }
}

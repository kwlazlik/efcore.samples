using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp1.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivalCategoty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Years = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivalCategoty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cid = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    IsSealed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ArchivalCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentationType_ArchivalCategoty_ArchivalCategoryId",
                        column: x => x.ArchivalCategoryId,
                        principalTable: "ArchivalCategoty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoxDocumentationTypeInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    BoxId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxDocumentationTypeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxDocumentationTypeInfo_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxDocumentationTypeInfo_DocumentationType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DocumentationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackageDocumentationTypeInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    PackageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDocumentationTypeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageDocumentationTypeInfo_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackageDocumentationTypeInfo_DocumentationType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DocumentationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxDocumentationTypeInfo_BoxId",
                table: "BoxDocumentationTypeInfo",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxDocumentationTypeInfo_TypeId",
                table: "BoxDocumentationTypeInfo",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationType_ArchivalCategoryId",
                table: "DocumentationType",
                column: "ArchivalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageDocumentationTypeInfo_PackageId",
                table: "PackageDocumentationTypeInfo",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageDocumentationTypeInfo_TypeId",
                table: "PackageDocumentationTypeInfo",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxDocumentationTypeInfo");

            migrationBuilder.DropTable(
                name: "PackageDocumentationTypeInfo");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "DocumentationType");

            migrationBuilder.DropTable(
                name: "ArchivalCategoty");
        }
    }
}

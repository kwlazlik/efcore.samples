﻿// <auto-generated />
using System;
using EFC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFC.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.School.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DbCreatedAt");

                    b.Property<string>("DbCreatedBy");

                    b.Property<DateTime>("DbModifiedAt");

                    b.Property<string>("DbModifiedBy");

                    b.Property<string>("DifficultyKey")
                        .IsRequired();

                    b.Property<int?>("SubjectId");

                    b.Property<long>("Time");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyKey");

                    b.HasIndex("SubjectId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Domain.School.ExamDifficulty", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(16);

                    b.Property<int>("Order");

                    b.Property<string>("Value");

                    b.HasKey("Key");

                    b.ToTable("ExamDifficulty");

                    b.HasData(
                        new { Key = "Easy", Order = 1, Value = "latwy" },
                        new { Key = "Medium", Order = 2, Value = "sredni" },
                        new { Key = "Hard", Order = 3, Value = "trudny" }
                    );
                });

            modelBuilder.Entity("Domain.School.Grade", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(16);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<string>("Value");

                    b.HasKey("Key");

                    b.ToTable("Grade");

                    b.HasDiscriminator<string>("Discriminator").HasValue("m");

                    b.HasData(
                        new { Key = "A", Order = 1, Value = "bardzodobry" },
                        new { Key = "B", Order = 2, Value = "dobry" },
                        new { Key = "C", Order = 3, Value = "dostateczny" },
                        new { Key = "D", Order = 4, Value = "dopuszczający" },
                        new { Key = "E", Order = 5, Value = "niedostateczny" }
                    );
                });

            modelBuilder.Entity("Domain.School.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("Domain.School.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DbCreatedAt");

                    b.Property<string>("DbCreatedBy");

                    b.Property<DateTime>("DbModifiedAt");

                    b.Property<string>("DbModifiedBy");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("SchoolClassId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.School.StudentExamGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DbCreatedAt");

                    b.Property<string>("DbCreatedBy");

                    b.Property<DateTime>("DbModifiedAt");

                    b.Property<string>("DbModifiedBy");

                    b.Property<int?>("ExamId");

                    b.Property<string>("GradeKey")
                        .IsRequired();

                    b.Property<int?>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("GradeKey");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentExams");
                });

            modelBuilder.Entity("Domain.School.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DbCreatedAt");

                    b.Property<string>("DbCreatedBy");

                    b.Property<DateTime>("DbModifiedAt");

                    b.Property<string>("DbModifiedBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Domain.School.BaadGrade", b =>
                {
                    b.HasBaseType("Domain.School.Grade");


                    b.ToTable("BaadGrade");

                    b.HasDiscriminator().HasValue("b");
                });

            modelBuilder.Entity("Domain.School.GoodGrade", b =>
                {
                    b.HasBaseType("Domain.School.Grade");


                    b.ToTable("GoodGrade");

                    b.HasDiscriminator().HasValue("g");
                });

            modelBuilder.Entity("Domain.School.Exam", b =>
                {
                    b.HasOne("Domain.School.ExamDifficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.School.Subject", "Subject")
                        .WithMany("Exams")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Domain.School.Student", b =>
                {
                    b.HasOne("Domain.School.SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId");
                });

            modelBuilder.Entity("Domain.School.StudentExamGrade", b =>
                {
                    b.HasOne("Domain.School.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId");

                    b.HasOne("Domain.School.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.School.Student", "Student")
                        .WithMany("ExamGrades")
                        .HasForeignKey("StudentId");
                });
#pragma warning restore 612, 618
        }
    }
}

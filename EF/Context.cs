using System;
using Domain.School;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFC
{
   public class Context : DbContext
   {
      public DbSet<Exam> Exams { get; set; }
      public DbSet<Student> Students { get; set; }
      public DbSet<StudentExamGrade> StudentExams { get; set; }
      public DbSet<Subject> Subjects { get; set; }
      public DbSet<SchoolClass> SchoolClasses { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder
           .UseSqlServer(@"Server=KOMPUTEREK\SQLEXPRESS;Database=testdb;Trusted_Connection=True;")
           .ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.QueryClientEvaluationWarning))
           .EnableSensitiveDataLogging();
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         //modelBuilder.Entity<Exam>().OwnsOne(e => e.Difficulty).Property(d => d.Value).HasColumnName(nameof(Exam.Difficulty));
         modelBuilder.Entity<Exam>().Property(e => e.Difficulty).HasConversion(new ExamDifficultyToStringConverter());
         modelBuilder.Entity<Exam>().Property(e => e.Time).HasConversion(v => v.Ticks, v => new TimeSpan(v));

         modelBuilder.Entity<StudentExamGrade>().OwnsOne(e => e.Grade).Property(d => d.Value).HasColumnName(nameof(StudentExamGrade.Grade));
         //modelBuilder.Entity<StudentExam>().Property(e => e.Grade).HasColumnName(nameof(StudentExam.Grade)).HasConversion(e => e.Value, s => new StudentExamGrade(s));
      }
   }

   internal class ExamDifficultyToStringConverter : ValueConverter<ExamDifficulty, string>
   {
      public ExamDifficultyToStringConverter(ConverterMappingHints mappingHints = null)
         : base(v => v.Value, v => new ExamDifficulty(v), mappingHints) { }
   }
}

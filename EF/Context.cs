using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain;
using Domain.School;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

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

         modelBuilder.EntityEnumeration<ExamDifficulty>();

         modelBuilder.EntityEnumeration<Grade>().HasDiscriminator().HasValue<GoodGrade>("gg").HasValue<MediumGrade>("mg").HasValue<BadGrade>("bg");
         modelBuilder.EntityEnumeration<GoodGrade, Grade>();
         modelBuilder.EntityEnumeration<MediumGrade, Grade>();
         modelBuilder.EntityEnumeration<BadGrade, Grade>();


         modelBuilder.Entity<Exam>().HasOne(e => e.Difficulty).WithMany().IsRequired();
         modelBuilder.Entity<Exam>().Property(e => e.Time).HasConversion(v => v.Ticks, v => new TimeSpan(v));
         modelBuilder.Entity<Exam>().Property(e => e.Flag).HasConversion<string>();


         modelBuilder.Entity<StudentExamGrade>().HasOne(e => e.Grade).WithMany().IsRequired();

         modelBuilder.HasSequence("testsequence");

         modelBuilder.Entity<Exam>().OwnsOne(e => e.Identifier).Property(i => i.Value).HasDefaultValueSql("'Ident #' + CAST(NEXT VALUE FOR testsequence AS varchar(max))");
      }

      public override int SaveChanges()
      {
         foreach (EntityEntry entityEntry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity.GetType().BaseType.IsGenericType && e.Entity.GetType().BaseType?.GetGenericTypeDefinition() == typeof(Enumeration<>)))
         {
            entityEntry.State = EntityState.Unchanged;
         }

         return base.SaveChanges();
      }
   }
}

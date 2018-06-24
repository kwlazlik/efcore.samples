using System;
using System.Linq;
using AutoMapper;
using Domain.School;
using EFC;
using EFC.Interception;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Core
{
   public class StdentViewModel
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public int ExamGradessCount { get; set; }
   }

   public class ClassViewModel
   {
      public string Number { get; set; }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         Mapper.Initialize(c => { c.CreateMap<Student, StdentViewModel>(); });


         using (var context = new Context())
         {
           // context.Database.EnsureDeleted();
           // context.Database.EnsureCreated();

            Subject math = new Subject
            {
               Name = "math"
            };

            Exam mathExam = new Exam
            {
               Difficulty = ExamDifficulty.Hard,
               Subject = math,
               Title = "Algebra exam",
               Time = new TimeSpan(0, 2, 30, 0)
            };

            Exam programmingExam = new Exam
            {
               Difficulty = ExamDifficulty.Medium,
               Subject = math,
               Title = "C# programming",
               Time = new TimeSpan(0, 1, 30, 0)
            };

            StudentExamGrade mathExamGrade = new StudentExamGrade
            {
               Exam = mathExam,
               Grade = Grade.A
            };

            StudentExamGrade programmingExamGrade = new StudentExamGrade
            {
               Exam = programmingExam,
               Grade = Grade.B
            };

            Student student = new Student
            {
               FirstName = "ala",
               LastName = "makota"
            }.AddExamGrades(mathExamGrade, programmingExamGrade);


            SchoolClass cls = new SchoolClass
            {
               Number = "1A"
            }.AddStudents(student);

            context.Add(cls);

            context.SaveChanges();
         }

         using (var context = new Context())
         {
            //var sql = context.Exams.FromSql("select * from Exams").Select(e => e.Title).ToList();

            TimeSpan ts = new TimeSpan(0, 3, 0, 0);
            var exam = context.Exams
              //.FixExpression(new DebugExpressionVisitor())
              .Where(e => e.Time > ts)
              .ToList();

            // var exam2 = context.Exams
            //  // .FixExpression(new DebugExpressionVisitor())
            //   .Where(e => e.Difficulty == ExamDifficulty.Hard)
            //   .ToList();


         }
      }
   }
}

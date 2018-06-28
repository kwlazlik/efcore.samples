using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.School;
using EFC;
using EFC.Interception;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Core
{
   public class ExamDifficultyViewModel
   {
      public string Key { get; set; }

      public string Value { get; set; }
   }

   public class ExamViewModel
   {
      public string Title { get; set; }

      public ExamDifficultyViewModel Difficulty { get; set; }
   }

   public class StudentViewModel
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public int ExamGradesCount { get; set; }
   }

   public class ClassViewModel
   {
      public string Number { get; set; }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         var xw = ExamDifficulty.List;

         Mapper.Initialize(c =>
         {
            c.CreateMap<Student, StudentViewModel>();
            c.CreateMap<Exam, ExamViewModel>();
            c.CreateMap<ExamDifficulty, ExamDifficultyViewModel>();
         });

         using (var context = new Context())
         {
            var x = context.Exams
              .Where(e => e.Difficulty == ExamDifficulty.Hard)
              .ToList();

            foreach (var exam in x)
            {
               exam.Difficulty = ExamDifficulty.Easy;
            }
            
            context.SaveChanges();
         }

         using (var context = new Context())
         {
            var math = new Subject
            {
               Name = "math"
            };

            var mathExam = new Exam
            {
               Difficulty = ExamDifficulty.Hard,
               Subject = math,
               Title = "Algebra exam",
               Time = new TimeSpan(0, 2, 30, 0)
            };

            var programmingExam = new Exam
            {
               Difficulty = ExamDifficulty.Medium,
               Subject = math,
               Title = "C# programming",
               Time = new TimeSpan(0, 1, 30, 0)
            };

            var mathExamGrade = new StudentExamGrade
            {
               Exam = mathExam,
               Grade = Grade.A
            };

            var programmingExamGrade = new StudentExamGrade
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

            context.Update(cls);

            context.SaveChanges();
         }

         using (var context = new Context())
         {
            //działa
            var ts = new TimeSpan(0, 3, 0, 0);
            List<Exam> exam = context.Exams
              .FixExpression(new DebugExpressionVisitor())
              .Where(e => e.Time > ts)
              .ToList();

            //nie działa
            var exam2 = context.Exams
              .FixExpression(new DebugExpressionVisitor())
              .Where(e => e.Difficulty == ExamDifficulty.Hard)
              .ToList();

            //działa
            List<StudentViewModel> vm = context.Students.Select(s => new StudentViewModel
               {
                  ExamGradesCount = s.ExamGrades.Count(),
                  FirstName = s.FirstName,
                  LastName = s.LastName
               })
              .FixExpression(new DebugExpressionVisitor())
              .ToList();

            //działa ale Student.ExamGrades musi być IEnumerable<>
            List<StudentViewModel> vm2 = context.Students.ProjectTo<StudentViewModel>(new DebugExpressionVisitor()).ToList();
         }
      }
   }
}

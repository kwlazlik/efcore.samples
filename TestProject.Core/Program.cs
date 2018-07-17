using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;
using Domain.School;
using EFC;
using EFC.Interception;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Mapper.Initialize(c =>
         {
            c.CreateMap<Student, StudentViewModel>();
            c.CreateMap<Exam, ExamViewModel>();
            c.CreateMap<ExamDifficulty, ExamDifficultyViewModel>();
            c.IncludeSourceExtensionMethods(typeof(ExamExtension));
         });

         QueryViewMmodelsSamples();

         CustomEnumerationsSamples();

         UpdataingDataSample();

         QueryDataSamples();
      }

      private static void QueryDataSamples()
      {
         using (var context = new Context())
         {
            //działa
            var ts = new TimeSpan(0, 3, 0, 0);
            List<Exam> exam = context.Exams
              .FixExpression(new DebugExpressionVisitor())
              .Where(e => e.Time > ts)
              .ToList();

            //nie działa
            List<Exam> exam2 = context.Exams
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

            List<ExamViewModel> examViewModels = context.Exams.Where(e => e.Flag == ExamFlag.ExamFlag1).ProjectTo<ExamViewModel>().ToList();
         }
      }

      private static void QueryViewMmodelsSamples()
      {
         using (var context = new Context())
         {
            List<ExamViewModel> list = context.Exams.ProjectTo<ExamViewModel>().Where(vm => vm.SubjectName.StartsWith("math")).Decompile().ToList();
            List<ExamViewModel> list2 = context.Exams.Where(vm => vm.Subject.Name == "math").ProjectTo<ExamViewModel>().Decompile().ToList();

            int i = 0;
            List<ExamViewModel> list3 = context.Exams
               .UseAsDataSource()
               .For<ExamViewModel>()
               .OnEnumerated(vms =>
               {
                  foreach (ExamViewModel vm in vms.OfType<ExamViewModel>())
                  {
                     vm.Ignored = i++;
                  }
               })
               .Where(vm => vm.SubjectName.StartsWith("math"))
               .Decompile()
               .ToList();
         }
      }

      private static void UpdataingDataSample()
      {
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
               Time = new TimeSpan(0, 2, 30, 0),
               Identifier = ExamIdentifier.Empty
            };

            var programmingExam = new Exam
            {
               Difficulty = ExamDifficulty.Medium,
               Subject = math,
               Title = "C# programming",
               Time = new TimeSpan(0, 1, 30, 0),
               Identifier = new ExamIdentifier
               {
                  Value = "alamakota"
               }
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
      }

      private static void CustomEnumerationsSamples()
      {
         using (var context = new Context())
         {
            List<Exam> exams = context.Exams
              .Where(e => e.Difficulty == ExamDifficulty.Hard)
              .ToList();

            foreach (Exam exam in exams)
            {
               exam.Difficulty = ExamDifficulty.Easy;
            }

            context.SaveChanges();
         }
      }
   }
}

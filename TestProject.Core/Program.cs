using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DelegateDecompiler;
using Domain.School;
using EFC;
using EFC.Interception;

namespace TestProject.Core
{
   public class StdentViewModel
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public int ExamGradessCount { get; set; }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         Mapper.Initialize(c => { c.CreateMap<Student, StdentViewModel>(); });


         using (var context = new Context())
         {
            Subject math = new Subject
            {
               Name = "math"
            };

            Exam mathExam = new Exam
            {
               Difficulty = ExamDifficulty.Hard,
               Subject = math,
               Title = "Algebra exam"
            };

            Exam programmingExam = new Exam
            {
               Difficulty = ExamDifficulty.Medium,
               Subject = math,
               Title = "C# programming"
            };

            StudentExamGrade mathExamGrade = new StudentExamGrade
            {
               Exam = mathExam,
               Grade = Grade.A
            };

            StudentExamGrade programmingExamGrade = new StudentExamGrade
            {
               Exam = mathExam,
               Grade = Grade.B
            };

            Student student = new Student
            {
               FirstName = "ala",
               LastName = "makota"
            }.AddExamGrades(mathExamGrade, programmingExamGrade);

            context.Add(mathExam);
            context.SaveChanges();
         }

         using (var context = new Context())
         {
            var exam = context.Exams.FixExpression().Decompile().FirstOrDefault(e => e.Difficulty == ExamDifficulty.Hard);
         }
      }
   }
}

using System.Collections.Generic;

namespace Domain.School
{
   public class Student : AuditableEntity<Student>
   {
      private readonly List<StudentExamGrade> _examGrades = new List<StudentExamGrade>();

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public IReadOnlyList<StudentExamGrade> ExamGrades => _examGrades;

      public Student AddExamGrades(params StudentExamGrade[] examsGrade)
      {
         _examGrades.AddRange(examsGrade);

         return this;
      }
   }
}

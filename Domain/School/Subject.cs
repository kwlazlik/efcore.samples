using System.Collections.Generic;

namespace Domain.School
{
   public class Subject : AuditableEntity<Subject>
   {
      private readonly List<Exam> _exams = new List<Exam>();

      public string Name { get; set; }

      public IReadOnlyCollection<Exam> Exams => _exams;
   }
}

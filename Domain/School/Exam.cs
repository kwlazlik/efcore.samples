using System;

namespace Domain.School
{
   public class ExamIdentifier
   {
      public static ExamIdentifier Empty => new ExamIdentifier();

      public string Value { get; set; }
   }

   public class Exam : AuditableEntity<Exam>
   {
      public ExamIdentifier Identifier { get; set; }

      public string Title { get; set; }

      public ExamFlag Flag { get; set; }

      public ExamDifficulty Difficulty { get; set; }

      public Subject Subject { get; set; }

      public TimeSpan Time { get; set; }
   }
}

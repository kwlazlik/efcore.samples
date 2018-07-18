using System;
using DelegateDecompiler;

namespace Domain.School
{
   public class ExamIdentifier
   {
      public static ExamIdentifier Empty => new ExamIdentifier();

      public string Value { get; set; }
   }

   public interface IExam
   {
      ExamIdentifier Identifier { get; set; }

      string Title { get; set; }

      ExamFlag Flag { get; set; }

      ExamDifficulty Difficulty { get; set; }

      Subject Subject { get; set; }

      TimeSpan Time { get; set; }

      DateTime DbModifiedAt { get; }

      string DbModifiedBy { get; }

      DateTime DbCreatedAt { get; }

      string DbCreatedBy { get; }

      int Id { get; }
   }

   public class Exam : AuditableEntity<Exam>, IExam
   {
      public ExamIdentifier Identifier { get; set; }

      public string Title { get; set; }

      public ExamFlag Flag { get; set; }

      public ExamDifficulty Difficulty { get; set; }

      public Subject Subject { get; set; }

      public TimeSpan Time { get; set; }
   }

   public static class ExamExtension
   {
      [Computed]
      public static string TitleExt(this IExam exam) => exam.Title + "alamakota";
   }
}

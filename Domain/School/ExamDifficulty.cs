namespace Domain.School
{
   public class ExamDifficulty : Enumeration<ExamDifficulty, string>
   {
      public ExamDifficulty(string value) : base(value) { }
      public ExamDifficulty(string value, string displayName) : base(value, displayName) { }

      public static ExamDifficulty Hard => new ExamDifficulty(nameof(Hard), "trudny");
      public static ExamDifficulty Medium => new ExamDifficulty(nameof(Medium), "sredni");
      public static ExamDifficulty Easy => new ExamDifficulty(nameof(Easy), "latwy");

      public static bool operator ==(ExamDifficulty left, string right)
      {
         return left.Value == right;
      }

      public static bool operator !=(ExamDifficulty left, string right)
      {
         return !(left == right);
      }
   }
}

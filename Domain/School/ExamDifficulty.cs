namespace Domain.School
{
   public class ExamDifficulty : Enumeration<ExamDifficulty>
   {
      public static readonly ExamDifficulty Easy = new ExamDifficulty(nameof(Easy), "latwy", 1);
      public static readonly ExamDifficulty Medium = new ExamDifficulty(nameof(Medium), "sredni", 2);
      public static readonly ExamDifficulty Hard = new ExamDifficulty(nameof(Hard), "trudny", 3);

      private ExamDifficulty(string key, string value, int order) : base(key, value, order) { } }
}

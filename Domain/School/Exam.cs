namespace Domain.School
{
   public class Exam : AuditableEntity<Exam>
   {
      public string Title { get; set; }
      public ExamDifficulty Difficulty { get; set; }
      public Subject Subject { get; set; }
   }
}

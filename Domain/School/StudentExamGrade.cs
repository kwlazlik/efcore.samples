namespace Domain.School
{
   public class StudentExamGrade : AuditableEntity<StudentExamGrade>
   {
      public Grade Grade { get; set; }
      public Student Student { get; set; }
      public Exam Exam { get; set; }
   }
}

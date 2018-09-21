using System;

namespace TestProject.Core.ProjectToVsUseAsDataSourceResult
{
   public class ExamViewModel
   {
      public string Title { get; set; }

      public TimeSpan Time { get; set; }

      public SubjectViewModel Subject { get; set; }
   }

   public class ExamViewModel2
   {
      public string Title { get; set; }

      public TimeSpan Time { get; set; }

      public string SubjectName { get; set; }
   }
}

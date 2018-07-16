using AutoMapper;
using Domain.School;

namespace TestProject.Core.ViewModels
{
   public class ExamViewModel
   {
      public string Title { get; set; }

      public ExamDifficultyViewModel Difficulty { get; set; }

      public ExamFlag Flag { get; set; }

      public string SubjectName { get; set; }

      [IgnoreMap]
      public int Ignored { get; set; }
   }
}

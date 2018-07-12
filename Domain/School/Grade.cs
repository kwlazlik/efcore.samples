namespace Domain.School
{
   public class Grade : Enumeration<Grade>
   {
      public static readonly GoodGrade A = new GoodGrade(nameof(A), "bardzodobry", 1);
      public static readonly GoodGrade B = new GoodGrade(nameof(B), "dobry", 2);
      public static readonly MediumGrade C = new MediumGrade(nameof(C), "dostateczny", 3);
      public static readonly BadGrade D = new BadGrade(nameof(D), "dopuszczający", 4);
      public static readonly BadGrade E = new BadGrade(nameof(E), "niedostateczny", 5);

      protected Grade(string key, string value, int order) : base(key, value, order) { }
   }

   public class GoodGrade : Grade
   {
      public new static readonly GoodGrade A = Grade.A;
      public new static readonly GoodGrade B = Grade.B;

      protected internal GoodGrade(string key, string value, int order) : base(key, value, order) { }
   }

   public class MediumGrade : Grade
   {
      public new static readonly MediumGrade C = Grade.C;

      protected internal MediumGrade(string key, string value, int order) : base(key, value, order) { }
   }

   public class BadGrade : Grade
   {
      public new static readonly BadGrade D = Grade.D;
      public new static readonly BadGrade E = Grade.E;

      protected internal BadGrade(string key, string value, int order) : base(key, value, order) { }
   }
}

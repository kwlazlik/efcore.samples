namespace Domain.School
{
   public class Grade : Enumeration<Grade>
   {
      public static readonly Grade A = new Grade(nameof(A), "bardzodobry", 1);
      public static readonly Grade B = new Grade(nameof(B), "dobry", 2);
      public static readonly Grade C = new Grade(nameof(C), "dostateczny", 3);
      public static readonly Grade D = new Grade(nameof(D), "dopuszczający", 4);
      public static readonly Grade E = new Grade(nameof(E), "niedostateczny", 5);

      protected Grade(string key, string value, int order) : base(key, value, order) { }
   }

   public class GoodGrade : Grade
   {
      public static readonly Grade A = Grade.A;
      public static readonly Grade B = Grade.B;

      protected internal GoodGrade(string key, string value, int order) : base(key, value, order) { }
   }

   public class BaadGrade : Grade
   {
      public static readonly Grade D = Grade.D;
      public static readonly Grade E = Grade.E;

      protected internal BaadGrade(string key, string value, int order) : base(key, value, order) { }
   }

}

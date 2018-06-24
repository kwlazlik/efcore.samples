namespace Domain.School
{
   public class Grade : Enumeration<Grade, string>
   {
      public Grade(string value) : base(value) { }
      public Grade(string value, string displayName) : base(value, displayName) { }

      public static Grade A => new Grade(nameof(A), "bardzodobry");
      public static Grade B => new Grade(nameof(B), "dobry");
      public static Grade C => new Grade(nameof(C), "dostateczny");
      public static Grade D => new Grade(nameof(D), "dopuszczający");
      public static Grade E => new Grade(nameof(E), "niedostateczny");
   }
}

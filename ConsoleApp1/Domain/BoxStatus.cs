namespace ConsoleApp2.Domain
{
   public sealed class BoxStatus : Enumeration<BoxStatus, string>
   {
      public static BoxStatus New = new BoxStatus(nameof(New), "Nowy");
      public static BoxStatus Used = new BoxStatus(nameof(Used), "Używany");

      // ReSharper disable once UnusedMember.Local
      private BoxStatus(string value) : base(value) { }

      private BoxStatus(string value, string displayName) : base(value.ToLowerInvariant(), displayName) { }
   }
}

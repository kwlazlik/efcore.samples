namespace Domain
{
   public sealed class BoxStatus : EnumerationString<BoxStatus>
   {
      public static BoxStatus New => new BoxStatus(nameof(New), "Nowy");
      public static BoxStatus Used => new BoxStatus(nameof(Used), "Używany");

      // ReSharper disable once UnusedMember.Local
      private BoxStatus(string value) : base(value) { }

      private BoxStatus(string value, string displayName) : base(value, displayName) { }
   }
}

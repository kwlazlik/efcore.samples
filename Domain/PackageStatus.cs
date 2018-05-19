namespace Domain
{
   public sealed class PackageStatus : EnumerationString<PackageStatus>
   {
      public static PackageStatus New => new PackageStatus(nameof(New), "Nowa");
      public static PackageStatus Sent => new PackageStatus(nameof(Sent), "Wysłana");
      public static PackageStatus Received => new PackageStatus(nameof(Received), "Odebrana");

      // ReSharper disable once UnusedMember.Local
      private PackageStatus(string value) : base(value) { }

      private PackageStatus(string value, string displayName) : base(value, displayName) { }
   }
}

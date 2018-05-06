namespace ConsoleApp1.Domain
{
   public sealed class PackageStatus : Enumeration<PackageStatus, string>
   {
      public static PackageStatus New { get; } = new PackageStatus("New", "Nowa");
      public static PackageStatus Sent { get; } = new PackageStatus("Sent", "Wysłana");
      public static PackageStatus Received { get; } = new PackageStatus("Received", "Odebrana");

      private PackageStatus(string value) : base(value) { }

      private PackageStatus(string value, string displayName) : base(value, displayName) { }
   }
}
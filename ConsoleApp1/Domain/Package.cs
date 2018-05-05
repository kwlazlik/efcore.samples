namespace ConsoleApp1
{
   public class Package : Entity<Package, int>
   {
      public string Number { get; set; }

      public PackageStatus Status { get; set; }
   }
}
using System.Linq;

namespace ConsoleApp1
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Package package1 = new Package
         {
            Number = "12345alamakota",
            Status = PackageStatus.New
         };

         Package package2 = new Package
         {
            Number = "12345alamakota 2",
            Status = PackageStatus.Sent
         };

         using (Context context = new Context())
         {
            context.Add(package1);
            context.Add(package2);
            context.SaveChanges();
         }

         using (Context context = new Context())
         {
            var packages = context.Packages.Where(p => p.Status.Equals(PackageStatus.Sent)).ToList();
            var packages2Sql = context.Packages.Where(p => Equals(p.Status, PackageStatus.Sent)).ToSql();
            var packages2Sql2 = context.Packages.Where(p => p.Status == PackageStatus.Sent.Value).ToSql();
            var packages2Sql22 = context.Packages.Where(p => p.Status.Value == PackageStatus.Sent.Value).ToSql();
            var packages2Sql222 = context.Packages.Where(p => p.Status.Equals(PackageStatus.Sent)).ToSql();
         }
      }
   }
}
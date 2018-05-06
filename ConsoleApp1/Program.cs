using System.Linq;
using AutoMapper.QueryableExtensions;
using ConsoleApp1.Domain;
using ConsoleApp1.EntityFramework;

namespace ConsoleApp1   
{
   public class BoxDto
   {
      public string Cid { get; set; }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         var package1 = Package.New();

         var package2 = Package.New();
         package2.Send(new PackageNumber("numer paczki 2"));

         using (var context = new Context())
         {
            context.Add(package1);
           // context.Add(package2);
            context.SaveChanges();
         }
//
//         using (var context = new Context())
//         {
//            //            var packages = context.Packages.Where(p => p.Status.Equals(PackageStatus.Sent)).ToList();
//            //            var packages2Sql = context.Packages.Where(p => Equals(p.Status, PackageStatus.Sent)).ToSql();
//            //            var packages2Sql2 = context.Packages.Where(p => p.Status == PackageStatus.Sent.Value).ToSql();
//            //            var packages2Sql22 = context.Packages.Where(p => p.Status.Value == PackageStatus.Sent.Value).ToSql();
//            //            var packages2Sql222 = context.Packages.Where(p => p.Status.Equals(PackageStatus.Sent)).ToSql();
//         }

         Box box1 = Box.New("123456789012").Value;
         Box box2 = Box.New("000000000000").Value;

         using (Context c = new Context())
         {
            c.Add(box1);
            c.Add(box2);
            c.SaveChanges();
         }

         AutoMapper.Mapper.Initialize(x => x.CreateMap<Box, BoxDto>());

         using (Context c = new Context())
         {
            string s = c.Boxes.Where(b => b.Cid.Value == "000000000000").ToSql();
            string s1 = c.Boxes.Where(b => b.Cid.Value == "000000000000").ProjectTo<BoxDto>().ToSql();

            var a = c.Boxes.Where(b => b.Cid.Value == "000000000000").ToList();
            var a1 = c.Boxes.Where(b => b.Cid.Value == "000000000000").ProjectTo<BoxDto>().ToList();
         }
      }
   }
}

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;
using Domain;
using EFC;

namespace TestProject   
{
   public class BoxDto
   {
      public string Cid { get; set; }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         Package package1 = Package.Create(new PackageNumber("123"), PackageStatus.New);
         package1.Test = "alamakota";
         Package package2 = Package.Create(new PackageNumber("123"), PackageStatus.New);
         package2.Test = "alamakota 2";

         using (Context context = new Context())
         {
            context.Add(package1);
            context.Add(package2);
            context.SaveChanges();
         }

         Mapper.Initialize(x => x.CreateMap<Package, PackageDto>());

         using (Context context = new Context())
         {
            //string packages2Sql = context.Packages.Where(p => Equals(p.Status, PackageStatus.Sent)).ToSql();
            //string packages2Sql2 = context.Packages.Where(p => p.Status == PackageStatus.Sent.Value).ToSql();
            //string packages2Sql222 = context.Packages.Where(p => p.Status.Equals(PackageStatus.Sent)).ToSql();

//            string packages2Sql22 = context.Packages.Where(p => p.Status.Value == PackageStatus.New).ToSql();
//            List<Package> packages = context.Packages.Where(p => p.Status.Value == PackageStatus.New.Value).ToList();
//            List<Package> packages2 = context.Packages.Where(p => p.Status.Value.Equals(PackageStatus.New.Value)).ToList();

            List<Package> packages3 = context.Packages.Where(p => p.Status.Value.Equals(PackageStatus.New.Value)).Decompile().ToList();
            List<PackageDto> packages4 = context.Packages.Where(p => p.NumberStatus == "123new").ProjectTo<PackageDto>().Decompile().ToList();

            Package package = context.Packages.First(p => p.Status.Value == PackageStatus.New.Value);
            package.Send(new PackageNumber("2345678"));

            context.SaveChanges();
         }

         using (Context context = new Context())
         {
            List<Package> packages = context.Packages.Where(p => p.Status.Value.Equals(PackageStatus.Sent.Value)).Decompile().ToList();

         }

//         Box box1 = Box.New("123456789012").Value;
//         Box box2 = Box.New("000000000000").Value;
//
//         using (Context c = new Context())
//         {
//            c.Add(box1);
//            c.Add(box2);
//            c.SaveChanges();
//         }
//

//         using (Context c = new Context())
//         {
//           // string s = c.Boxes.Where(b => b.Cid.Value == "000000000000").ToSql();
//           // string s1 = c.Boxes.Where(b => b.Cid.Value == "000000000000").ProjectTo<BoxDto>().ToSql();
//
//            var a = c.Boxes.Where(b => b.Cid.Value == "000000000000").ToList();
//            var a1 = c.Boxes.Where(b => b.Cid.Value == "000000000000").ProjectTo<BoxDto>().ToList();
//         }
      }
   }
}

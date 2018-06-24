using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;
using Domain.DocumentationTypes;
using Domain.Hierarchy;
using Domain.Packages;
using EFC;

namespace TestProject
{
   public class BranchPackageViewModel
   {
      public string Number { get; set; }

      public string HierarchyUnitName { get; set; }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         Mapper.Initialize(x => { x.CreateMap<BranchPackage, BranchPackageViewModel>(); });

         using (var context = new Context())
         {
            ArchivalCategory archivalCategory = 
            DocumentationType documentationType = context.DocumentationTypes.FirstOrDefault(dt => dt.Name.Value == "dt1") ?? DocumentationType.Create()

            BranchPackage BranchPackage1 = BranchPackage.Create();
            BranchPackage BranchPackage2 = BranchPackage.Create(new PackageNumber("123"), BranchPackageStatus.New);

            //context.Add(BranchPackage1);
            context.Add(BranchPackage2);
            context.SaveChanges();
         }

         //         using (Context context = new Context())
         //         {
         //           // var a = context.BranchPackages.ProjectTo<BranchPackageViewModel>().ToList();
         //            string s = "NEW";
         //            string n = "123";
         //            var a2 = context.BranchPackages.Where(p => ((string)p.Number) == s).ToList();
         //
         //         }

         using (var context = new Context())
         {
            string BranchPackages2Sql = context.BranchPackages.Where(p => Equals(p.Status, BranchPackageStatus.Sent)).ToSql();
            string BranchPackages2Sql2 = context.BranchPackages.Where(p => p.Status == BranchPackageStatus.Sent.Value).ToSql();
            string BranchPackages2Sql222 = context.BranchPackages.Where(p => p.Status.Equals(BranchPackageStatus.Sent)).ToSql();
            string BranchPackages2Sql22 = context.BranchPackages.Where(p => p.Status.Value == BranchPackageStatus.New).ToSql();
            string BranchPackages2Sql22C = context.BranchPackages.Where(p => p.Status.Value == BranchPackageStatus.New.Value).ToSql();
            IQueryable<BranchPackage> BranchPackages2Sql22asdc = context.BranchPackages.Where(p => p.Status == BranchPackageStatus.New);

            List<BranchPackage> BranchPackages = context.BranchPackages.Where(p => p.Status.Value == BranchPackageStatus.New.Value).ToList();
            List<BranchPackage> BranchPackages2 = context.BranchPackages.Where(p => p.Status.Value.Equals(BranchPackageStatus.New.Value)).ToList();

            List<BranchPackage> BranchPackages3 = context.BranchPackages.Where(p => p.Status.Value.Equals(BranchPackageStatus.New.Value)).Decompile().ToList();
            List<BranchPackageViewModel> BranchPackages4 =
               context.BranchPackages.Where(p => p.Number == "123new").ProjectTo<BranchPackageViewModel>().Decompile().ToList();

            context.SaveChanges();
         }
      }
   }
}

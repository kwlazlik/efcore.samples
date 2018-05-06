using ConsoleApp1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ConsoleApp1.EntityFramework
{
   public class Context : DbContext
   {
      public DbSet<Package> Packages { get; set; }

      public DbSet<Box> Boxes { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder
            .UseSqlServer(@"Server=komputerek\sqlexpress;Database=testdb;Trusted_Connection=True;")
            .ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.QueryClientEvaluationWarning))
            .EnableSensitiveDataLogging(true);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);



         modelBuilder.Entity<Package>(etb =>
         {
            etb.OwnsEnumeration(p => p.Status).IsRequired();
            etb.OwnsOne(p => p.Number).Property(pn => pn.Value).HasColumnName(nameof(Package.Number));
         });

         modelBuilder.Entity<Box>(builder =>
         {
            builder.OwnsEnumeration(p => p.Status).IsRequired();
            builder.OwnsOne(b => b.Cid).Property(c => c.Value).HasColumnName(nameof(Box.Cid));

           // builder.Property(b => b.Cid).HasConversion(cid => cid.Value, str => CidNumber.New(str).Value);
           // builder.Property(b => b.Cid).HasConversion<string>();
         });

         modelBuilder.Owned<CidNumber>();
         modelBuilder.Owned<PackageStatus>();
         modelBuilder.Owned<BoxStatus>();
      }
   }
}

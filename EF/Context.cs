using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EFC
{
   public class Context : DbContext
   {
      public DbSet<Package> Packages { get; set; }
      public DbSet<Order> Orders { get; set; }
      public DbSet<OrderForHire> OrderForHires { get; set; }
      public DbSet<OrderForScans> OrderForScans { get; set; }

      // public DbSet<Box> Boxes { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder
            .UseSqlServer(@"Server=localhost\sqlserver;Database=testdb;Trusted_Connection=True;")
            .ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.QueryClientEvaluationWarning))
            .EnableSensitiveDataLogging();
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Package>(etb =>
         {
            etb.OwnsOne(x => x.Status).Property(ps => ps.Value).IsRequired();
            etb.OwnsOne(p => p.Number).UsePropertyAccessMode(PropertyAccessMode.Field).Property(pn => pn.Value);
         });

         modelBuilder.Entity<OrderForHire>(e =>
         {
            e.OwnsEnumeration(o => o.Status).IsRequired();
         });

         modelBuilder.Entity<OrderForScans>(e =>
         {
            e.OwnsEnumeration(o => o.Status).IsRequired();
         });

         //         modelBuilder.Entity<Box>(builder =>
         //         {
         //            builder.OwnsEnumeration(p => p.Status).IsRequired();
         //            builder.OwnsOne(b => b.Cid).Property(c => c.Value).HasColumnName(nameof(Box.Cid));
         //
         //           // builder.Property(b => b.Cid).HasConversion(cid => cid.Value, str => CidNumber.New(str).Value);
         //           // builder.Property(b => b.Cid).HasConversion<string>();
         //         });

         //         modelBuilder.Owned<CidNumber>();
         //         modelBuilder.Owned<PackageStatus>();
         //         modelBuilder.Owned<BoxStatus>();
      }
   }
}

using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
   public class Context : DbContext
   {
      public DbSet<Package> Packages { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder
            .UseSqlServer(@"Server=komputerek\sqlexpress;Database=testdb;Trusted_Connection=True;")
            .ConfigureWarnings(wcb => wcb.Throw());
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Package>(etb =>
         {
            etb.OwnsEnumeration(p => p.Status).IsRequired();
         });

      }
   }
}
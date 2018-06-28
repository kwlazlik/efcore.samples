using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFC 
{
   public static class ModelBuilderExtensions
   {
      public static EntityTypeBuilder<TEnumeration> EntityEnumeration<TEnumeration>(this ModelBuilder modelBuilder) where TEnumeration : Enumeration<TEnumeration>
      {
         EntityTypeBuilder<TEnumeration> entityTypeBuilder = modelBuilder.Entity<TEnumeration>();
         entityTypeBuilder.HasKey(e => e.Key);
         entityTypeBuilder.Property(ed => ed.Key).HasMaxLength(16);
         entityTypeBuilder.Property(ed => ed.Value);
         entityTypeBuilder.Property(ed => ed.Order);
         entityTypeBuilder.HasData(Enumeration<TEnumeration>.List.Where(e => e.GetType() == typeof(TEnumeration)).ToArray());

         return entityTypeBuilder;
      }
   }
}
using System;

namespace ConsoleApp1.Domain
{
   public abstract class DocumentationTypeInfo<TEntity> : Entity<TEntity> where TEntity : Entity<TEntity, int>
   {
      public DocumentationType Type { get; protected set; }

      public DateTime ValidFrom { get; protected set; }

      public DateTime ValidTo { get; protected set; }

      public void UpdateType(DocumentationType type)
      {
         Type = type;
      }

      protected void UpdateValidFromTo(DateTime from, DateTime to)
      {
         ValidFrom = from;
         ValidTo = to;
      }
   }
}

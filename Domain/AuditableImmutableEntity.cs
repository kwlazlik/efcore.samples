using System;

namespace Domain
{
   public abstract class AuditableImmutableEntity<TEntity> : Entity<TEntity>, IAuditableImmutableEntity where TEntity : Entity<TEntity, int>
   {
      public DateTime DbCreatedAt { get; private set; }
      public string DbCreatedBy { get; private set; }

      void IAuditableImmutableEntity.AuditCreated(DateTime now, string user)
      {
         DbCreatedAt = now;
         DbCreatedBy = user;
      }
   }
}

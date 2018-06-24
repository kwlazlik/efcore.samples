using System;

namespace Domain
{
   public abstract class AuditableEntity<TEntity> : AuditableImmutableEntity<TEntity>, IAuditableEntity where TEntity : Entity<TEntity, int>
   {
      public DateTime DbModifiedAt { get; private set; }
      public string DbModifiedBy { get; private set; }

      void IAuditableEntity.AuditModified(DateTime now, string user)
      {
         DbModifiedAt = now;
         DbModifiedBy = user;
      }
   }
}

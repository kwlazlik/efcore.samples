using System;

namespace Domain
{
   public interface IAuditableEntity : IAuditableImmutableEntity
   {
      DateTime DbModifiedAt { get; }
      string DbModifiedBy { get; }

      void AuditModified(DateTime now, string user);
   }
}

using System;

namespace Domain
{
   public interface IAuditableImmutableEntity
   {
      DateTime DbCreatedAt { get; }

      string DbCreatedBy { get; }

      void AuditCreated(DateTime now, string user);
   }
}

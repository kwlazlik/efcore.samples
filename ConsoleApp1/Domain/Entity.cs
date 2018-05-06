using System;

namespace ConsoleApp1.Domain
{
   public abstract class Entity<TEntity> : Entity<TEntity, int> where TEntity : Entity<TEntity, int> { }

   public abstract class Entity<TEntity, TKey> : IEquatable<TEntity> where TEntity : Entity<TEntity, TKey> where TKey : IEquatable<TKey>
   {
      private int? _hashCode;

      public TKey Id { get; set; }

      public bool Equals(TEntity other) =>
         other != null && (Equals(other.Id, default(TKey)) && Equals(Id, default(TKey)) ? ReferenceEquals(other, this) : other.Id.Equals(Id));

      public override bool Equals(object obj) => Equals(obj as TEntity);

      public override int GetHashCode()
      {
         if (_hashCode.HasValue)
         {
            return _hashCode.Value;
         }

         bool thisIsTransient = Equals(Id, default(TKey));

         if (thisIsTransient)
         {
            _hashCode = base.GetHashCode();
            return _hashCode.Value;
         }

         return Id.GetHashCode();
      }
   }
}

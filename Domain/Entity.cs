using System;

namespace Domain
{
   public abstract class Entity<TEntity> : Entity<TEntity, int> where TEntity : Entity<TEntity, int> { }

   public abstract class Entity<TEntity, TKey> : IEquatable<TEntity> where TEntity : Entity<TEntity, TKey> where TKey : IEquatable<TKey>
   {
      private int? _hashCode;

      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      public TKey Id { get; private set; }

      public bool Equals(TEntity other) =>
         other != null && (Equals(other.Id, default) && Equals(Id, default) ? ReferenceEquals(other, this) : other.Id.Equals(Id));

      public override bool Equals(object obj) => Equals(obj as TEntity);

      public override int GetHashCode()
      {
         if (_hashCode.HasValue) return _hashCode.Value;

         bool thisIsTransient = Equals(Id, default);

         if (thisIsTransient)
         {
            _hashCode = base.GetHashCode();
            return _hashCode.Value;
         }

         return Id.GetHashCode();
      }

      public static bool operator ==(Entity<TEntity, TKey> left, Entity<TEntity, TKey> right) =>
         left is null && right is null || !(left is null) && left.Equals(right);

      public static bool operator !=(Entity<TEntity, TKey> left, Entity<TEntity, TKey> right) => !(left == right);
   }
}

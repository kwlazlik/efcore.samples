using System;

namespace ConsoleApp1
{
   public abstract class Entity<TEntity, TKey> : IEquatable<TEntity> where TEntity : Entity<TEntity, TKey> where TKey : IEquatable<TKey>
   {
      private int? _hashCode;

      public TKey Id { get; set; }

      public bool Equals(TEntity other)
      {
         if (other == null)
         {
            return false;
         }

         bool otherIsTransient = Equals(other.Id, default(TKey));
         bool thisIsTransient = Equals(Id, default(TKey));
         return otherIsTransient && thisIsTransient ? ReferenceEquals(other, this) : other.Id.Equals(Id);
      }

      public override bool Equals(object obj)
      {
         return Equals(obj as TEntity);
      }

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
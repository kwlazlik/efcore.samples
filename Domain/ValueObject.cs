using System;

namespace Domain
{
   public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
   {
      private static readonly ValueObjectEqualityComparer<T> EqualityComparer = new ValueObjectEqualityComparer<T>();

      public bool Equals(T other) => ReferenceEquals(this, other) || !(other is null) && EqualityComparer.Equals((T) this, other);

      public override bool Equals(object obj) => Equals(obj as T);

      public override int GetHashCode() => EqualityComparer.GetHashCode((T) this);

      public static bool operator ==(ValueObject<T> left, T right) => ReferenceEquals(left, right) || !(left is null) && !(right is null) && Equals(left, right);

      public static bool operator !=(ValueObject<T> left, T right) => !(left == right);

      public static bool operator ==(T left, ValueObject<T> right) => ReferenceEquals(left, right) || !(left is null) && !(right is null) && Equals(left, right);

      public static bool operator !=(T left, ValueObject<T> right) => !(left == right);
   }
}

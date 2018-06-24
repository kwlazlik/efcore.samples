using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Domain
{
   public abstract class ValueType<T> : IEquatable<ValueType<T>> where T : ValueType<T>
   {
      public bool Equals(ValueType<T> other) => other != null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

      public override bool Equals(object other) => Equals(other as ValueType<T>);

      public override int GetHashCode() =>
         GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
               unchecked
               {
                  return current * 23 + (obj?.GetHashCode() ?? 0);
               }
            });

      public static bool operator ==(T a, ValueType<T> b) => a is null && b is null || !(a is null) && !(b is null) && a.Equals(b);

      public static bool operator !=(T a, ValueType<T> b) => !(a == b);

      protected abstract IEnumerable<object> GetEqualityComponents();

      public static T Empty() => (T) FormatterServices.GetUninitializedObject(typeof(T));

      public bool IsEmpty() => GetEqualityComponents().All(c => c == default);
   }
}

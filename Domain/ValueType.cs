using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Domain
{
   public abstract class ValueType<TValueType> : IEquatable<ValueType<TValueType>> where TValueType : ValueType<TValueType>
   {
      public static TValueType Empty = (TValueType)FormatterServices.GetUninitializedObject(typeof(TValueType));

      public bool Equals(ValueType<TValueType> other) => other != null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

      public override bool Equals(object other) => Equals(other as ValueType<TValueType>);

      public override int GetHashCode() => GetEqualityComponents()
        .Aggregate(1, (current, value) =>
         {
            unchecked
            {
               return current * 23 + (value?.GetHashCode() ?? 0);
            }
         });

      public static bool operator ==(TValueType a, ValueType<TValueType> b) => a is null && b is null || !(a is null) && !(b is null) && a.Equals(b);

      public static bool operator !=(TValueType a, ValueType<TValueType> b) => !(a == b);

      public bool IsEmpty() => Equals(Empty);

      protected abstract IEnumerable<object> GetEqualityComponents();
   }
}

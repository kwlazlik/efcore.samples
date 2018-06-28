using System.Collections.Generic;

namespace Domain
{
   public interface ISingleValueValueType<out TValue>
   {
      TValue Value { get; }
   }

   public abstract class SingleValueValueType<TValueType, TValue> : ValueType<TValueType>, ISingleValueValueType<TValue>
      where TValueType : ValueType<TValueType>
   {
      protected SingleValueValueType(TValue value) => Value = value;

      public TValue Value { get; }

      protected sealed override IEnumerable<object> GetEqualityComponents()
      {
         yield return Value;
      }
   }
}

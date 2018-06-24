using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Domain
{
   [Serializable]
   [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
   public abstract class Enumeration<TEnumeration, TValue> : SingleValueValueType<TEnumeration, TValue>, IComparable<TEnumeration>
      where TEnumeration : Enumeration<TEnumeration, TValue>
      where TValue : IComparable<TValue>, IEquatable<TValue>
   {
      private static readonly Dictionary<TValue, TEnumeration> ValuesToEnumerations = typeof(TEnumeration)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Select(pi => pi.GetValue(null))
        .OfType<TEnumeration>()
        .ToDictionary(e => e.Value, e => e);

      protected Enumeration(TValue value) : base(value)
      {
         DisplayName = ValuesToEnumerations[Value].DisplayName;
      }

      protected Enumeration(TValue value, string displayName) : base(value)
      {
         DisplayName = displayName;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string DebuggerDisplay => $"{Value}:{DisplayName}";

      public static IReadOnlyCollection<TEnumeration> All => ValuesToEnumerations.Values;

      public string DisplayName { get; }

      public int CompareTo(TEnumeration other) => Value.CompareTo(other.Value);

      public override string ToString() => DisplayName;

      public static string DisplayNameFromValue(TValue value) => ValuesToEnumerations.TryGetValue(value, out TEnumeration enumeration) ? enumeration.DisplayName : "-";

      public static TEnumeration FromValue(TValue value) => ValuesToEnumerations[value];
   }
}

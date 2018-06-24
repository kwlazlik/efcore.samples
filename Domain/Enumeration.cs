using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Domain
{
   [Serializable]
   [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
   public abstract class Enumeration<TEnumeration, TValue> : ValueType<TEnumeration>, IComparable<TEnumeration>, ISingleValueValueType<TValue>
      where TEnumeration : Enumeration<TEnumeration, TValue>
      where TValue : IComparable<TValue>, IEquatable<TValue>
   {
      private static readonly Dictionary<TValue, string> ValuesToDisplayNames = typeof(TEnumeration)
                                                                                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                                                                                .Select(pi => pi.GetValue(null))
                                                                                .OfType<TEnumeration>()
                                                                                .ToDictionary(e => e.Value, e => e.DisplayName);

      private static readonly Dictionary<TValue, TEnumeration> ValuesToEnumerations = typeof(TEnumeration)
                                                                                      .GetProperties(BindingFlags.Public | BindingFlags.Static)
                                                                                      .Select(pi => pi.GetValue(null))
                                                                                      .OfType<TEnumeration>()
                                                                                      .ToDictionary(e => e.Value, e => e);

      private static readonly List<TEnumeration> Enumerations = typeof(TEnumeration)
                                                                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                                                                .Select(pi => pi.GetValue(null))
                                                                .OfType<TEnumeration>()
                                                                .ToList();

      protected Enumeration(TValue value)
      {
         Value = value;
         DisplayName = ValuesToDisplayNames[Value];
      }

      protected Enumeration(TValue value, string displayName)
      {
         Value = value;
         DisplayName = displayName;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string DebuggerDisplay => $"{Value}:{DisplayName}";

      public static IReadOnlyCollection<TEnumeration> All => Enumerations;

      public string DisplayName { get; }

      public int CompareTo(TEnumeration other) => Value.CompareTo(other.Value);

      public TValue Value { get; }

      public override string ToString() => DisplayName;

      public static string DisplayNameForValue(TValue value) => ValuesToDisplayNames.TryGetValue(value, out string displayName) ? displayName : "-";

      public static TEnumeration FromValue(TValue value) => ValuesToEnumerations[value];

      protected override IEnumerable<object> GetEqualityComponents()
      {
         yield return Value;
      }
   }
}

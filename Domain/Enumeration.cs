using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Domain
{
   [Serializable]
   [DebuggerDisplay("{DebuggerDisplay,nq}")]
   public abstract class Enumeration<TEnumeration, TValue> : ValueObject<TEnumeration>, IComparable<TEnumeration>
         where TEnumeration : Enumeration<TEnumeration, TValue>
         where TValue : IComparable<TValue>, IEquatable<TValue>
   {
      private static readonly Dictionary<TValue, string> ValuesToDisplayNames = typeof(TEnumeration)
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Select(pi => pi.GetValue(null))
         .OfType<TEnumeration>()
         .ToDictionary(e => e.Value, e => e.DisplayName);

      private static readonly List<TEnumeration> Enumerations = typeof(TEnumeration)
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Select(pi => pi.GetValue(null))
         .OfType<TEnumeration>()
         .ToList();

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string DebuggerDisplay => $"{Value}:{DisplayName}";

      public static IReadOnlyCollection<TEnumeration> All => Enumerations;

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

      public TValue Value { get; }

      public string DisplayName { get; }

      public override string ToString() => DisplayName;

      public int CompareTo(TEnumeration other) => Value.CompareTo(other.Value);
   }
}

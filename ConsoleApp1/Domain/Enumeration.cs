using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ConsoleApp2.Domain
{
   [Serializable]
   [DebuggerDisplay("{Value}:{DisplayName}")]
   public abstract class Enumeration<TEnumeration, TValue> : IComparable<TEnumeration>, IEquatable<TEnumeration>, IComparable
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

      public static IReadOnlyCollection<TEnumeration> All => Enumerations;


      public int CompareTo(TEnumeration other) => Value.CompareTo(other.Value);

      public override string ToString() => DisplayName;

      public override bool Equals(object obj) => Equals(obj as TEnumeration);

      public bool Equals(TEnumeration other) => other != null && Value.Equals(other.Value);

      public override int GetHashCode() => Value.GetHashCode() ^ DisplayName.GetHashCode() ^ GetType().GetHashCode();

      public int CompareTo(object obj) => CompareTo((TEnumeration)obj);

      public static bool operator ==(TEnumeration left, Enumeration<TEnumeration, TValue> right) => left != null && left.Equals(right as TEnumeration);

      public static bool operator !=(TEnumeration left, Enumeration<TEnumeration, TValue> right) => !(left == right);
   }
}

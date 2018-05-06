using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1.Domain
{
   [Serializable]
   [DebuggerDisplay("{Value}:{DisplayName}")]
   public abstract class Enumeration<TEnumeration, TValue> : IComparable<TEnumeration>, IEquatable<TEnumeration>, IComparable
         where TEnumeration : Enumeration<TEnumeration, TValue>
         where TValue : IComparable<TValue>, IEquatable<TValue>
   {
      private static readonly Dictionary<TValue, string> ValuesToDisplayNames = new Dictionary<TValue, string>();
      private static readonly List<TEnumeration> Enumerations = new List<TEnumeration>();

      protected Enumeration(TValue value)
      {
         Value = value;
         DisplayName = ValuesToDisplayNames[Value];
      }

      protected Enumeration(TValue value, string displayName)
      {
         Value = value;
         DisplayName = displayName;

         ValuesToDisplayNames.Add(Value, DisplayName);
         Enumerations.Add((TEnumeration)this);
      }

      public TValue Value { get; }

      public string DisplayName { get; }

      public static IEnumerable<TEnumeration> GetAll() => Enumerations;

      public int CompareTo(TEnumeration other) => Value.CompareTo(other.Value);

      public sealed override string ToString() => DisplayName;

      public override bool Equals(object obj) => Equals(obj as TEnumeration);

      public bool Equals(TEnumeration other) => other != null && Value.Equals(other.Value);

      public override int GetHashCode() => Value.GetHashCode() ^ DisplayName.GetHashCode() ^ GetType().GetHashCode();

      public int CompareTo(object obj) => CompareTo((TEnumeration)obj);
   }
}

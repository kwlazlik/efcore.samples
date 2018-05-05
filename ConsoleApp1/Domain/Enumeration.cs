using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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

      public static IEnumerable<TEnumeration> GetAll()
      {
         return Enumerations;
      }

      public int CompareTo(TEnumeration other)
      {
         return Value.CompareTo(other.Value);
      }

      public sealed override string ToString()
      {
         return DisplayName;
      }

      public override bool Equals(object obj)
      {
         return Equals(obj as TEnumeration);
      }

      public bool Equals(TEnumeration other)
      {
         return other != null && Value.Equals(other.Value);
      }

      public override int GetHashCode()
      {
         return Value.GetHashCode() ^ DisplayName.GetHashCode() ^ GetType().GetHashCode();
      }

      public int CompareTo(object obj)
      {
         return CompareTo((TEnumeration)obj);
      }
   }
}

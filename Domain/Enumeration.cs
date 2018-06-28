using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Domain 
{
   [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
   public abstract class Enumeration<TEnumeration> where TEnumeration : Enumeration<TEnumeration>
   {
      public static readonly IReadOnlyList<TEnumeration> List = typeof(TEnumeration)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(pi => pi.GetValue(null))
        .OfType<TEnumeration>()
        .ToList();

      protected Enumeration(string key, string value, int order)
      {
         Key = key;
         Value = value;
         Order = order;
      }

      public string Key { get; }

      public string Value { get; }

      public int Order { get; }

      public override string ToString() => $"{Key}:{Value}";

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string DebuggerDisplay => $"{typeof(TEnumeration).Name}|{Key}|{Value}|{Order}";
   }
}
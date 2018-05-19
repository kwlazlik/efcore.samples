using System;

namespace Domain
{
   public abstract class EnumerationString<TEnumeration> : Enumeration<TEnumeration, string> where TEnumeration : EnumerationString<TEnumeration>
   {
      protected EnumerationString(string value) : base(value)
      {
      }

      protected EnumerationString(string value, string displayName) : base(value.ToUpperInvariant(), displayName)
      {
      }
   }
}

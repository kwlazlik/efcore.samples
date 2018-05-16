using System;

namespace Domain
{
   public class PackageNumber : ValueObject<PackageNumber>
   {
      public string Value { get; }

      public PackageNumber(string value)
      {
         Value =  value ?? throw new ArgumentNullException(nameof(value));
      }

      public override string ToString() => Value;

      public static implicit operator string(PackageNumber cidNumber) => cidNumber.Value;
   }
}

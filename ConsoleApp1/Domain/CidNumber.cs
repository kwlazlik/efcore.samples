using System;

namespace ConsoleApp2.Domain
{
   public class CidNumber : ValueObject<CidNumber>
   {
      public string Value { get; }

      private CidNumber(string value)
      {
         Value =  value ?? throw new ArgumentNullException(nameof(value));

         if (!Validate(value))
         {
            throw new ArgumentException(value, nameof(value));
         }
      }

      public override string ToString() => Value;

      public static Result<CidNumber> Create(string cid)
      {
         return Validate(cid).ToResult(() => new CidNumber(cid), "Niepoprawny numer cid.");
      }

      public static implicit operator string(CidNumber cidNumber) => cidNumber.Value;

      private static bool Validate(string cid)
      {
         return cid.Length == 12;
      }
   }
}

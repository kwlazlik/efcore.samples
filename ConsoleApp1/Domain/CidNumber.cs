using System;

namespace ConsoleApp1.Domain
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

      public static Result<CidNumber> New(string cid)
      {
         return Validate(cid) ? Result.Ok(new CidNumber(cid)) : Result.Fail<CidNumber>("Niepoprawny numer cid.");
      }

      public static implicit operator string(CidNumber cidNumber) => cidNumber.Value;

      private static bool Validate(string cid)
      {
         return cid.Length == 12;
      }

      public static bool operator ==(CidNumber left, string right) => left?.Value == right;

      public static bool operator !=(CidNumber left, string right) => !(left == right);

      public static bool operator ==(string left, CidNumber right) => left == right?.Value;

      public static bool operator !=(string left, CidNumber right) => !(left == right);
   }
}

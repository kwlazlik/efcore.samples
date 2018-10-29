using System;

namespace Common.Results
{
   public static class ResultBoolExtensions
   {
      public static Result ToResult(this bool isOk, string error) => isOk ? Result.Ok() : Result.Fail(error);

      public static Result ToResult(this bool isOk, Func<string> error) => isOk ? Result.Ok() : Result.Fail(error());

      public static Result<TValue> ToResult<TValue>(this bool isOk, TValue value, string error) => isOk ? value : Result.Fail<TValue>(error);

      public static Result<TValue> ToResult<TValue>(this bool isOk, Func<TValue> value, string error) => isOk ? value() : Result.Fail<TValue>(error);

      public static Result<TValue> ToResult<TValue>(this bool isOk, Func<TValue> value, Func<string> error) =>
         isOk ? value() : Result.Fail<TValue>(error());
   }
}

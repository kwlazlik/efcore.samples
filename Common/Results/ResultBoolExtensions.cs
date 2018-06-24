using System;

namespace Common.Results
{
   public static class ResultBoolExtensions
   {
      public static Result ToResult(this bool isOk, string error) => isOk ? Result.Ok() : Result.Fail(error);

      public static Result ToResult(this bool isOk, Func<string> error) => isOk ? Result.Ok() : Result.Fail(error());

      public static Result<TValue> ToResult<TValue>(this bool isOk, TValue value, string error) => isOk ? Result.Ok(value) : Result.Fail<TValue>(error);

      public static Result<TValue> ToResult<TValue>(this bool isOk, Func<TValue> value, string error) => isOk ? Result.Ok(value()) : Result.Fail<TValue>(error);

      public static Result<TValue> ToResult<TValue>(this bool isOk, Func<TValue> value, Func<string> error) =>
         isOk ? Result.Ok(value()) : Result.Fail<TValue>(error());

      public static Result<TValue, TResult> ToResult<TValue, TResult>(this bool isOk, TValue value, TResult error) where TResult : class =>
         isOk ? Result.Ok<TValue, TResult>(value) : Result.Fail<TValue, TResult>(error);

      public static Result<TValue, TResult> ToResult<TValue, TResult>(this bool isOk, Func<TValue> value, TResult error) where TResult : class =>
         isOk ? Result.Ok<TValue, TResult>(value()) : Result.Fail<TValue, TResult>(error);

      public static Result<TValue, TResult> ToResult<TValue, TResult>(this bool isOk, Func<TValue> value, Func<TResult> error) where TResult : class =>
         isOk ? Result.Ok<TValue, TResult>(value()) : Result.Fail<TValue, TResult>(error());
   }
}

using System;

// ReSharper disable InconsistentNaming

namespace ConsoleApp2.Domain
{
   public static class ResultExtensions
   {
      // Result

      public static Result OnSuccess(this Result result, Func<Result> func)
      {
         return result ? func() : result;
      }

      public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
      {
         return result ? Result.Ok(func()) : Result.Fail<T>(result.Error);
      }

      public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
      {
         return result ? func() : Result.Fail<T>(result.Error);
      }

      public static Result OnSuccess(this Result result, Action action)
      {
         if (result)
         {
            action();
         }

         return result;
      }

      public static Result OnFailure(this Result result, Action action)
      {
         if (result.IsFail)
         {
            action();
         }

         return result;
      }

      public static Result OnFailure(this Result result, Action<string> action)
      {
         if (result.IsFail)
         {
            action(result.Error);
         }

         return result;
      }

      public static T OnBoth<T>(this Result result, Func<Result, T> func)
      {
         return func(result);
      }

      public static Result OnBoth(this Result result, Action action)
      {
         action();
         return result;
      }

      public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
      {
         if (result.IsFail)
         {
            return Result.Fail(result.Error);
         }

         if (!predicate())
         {
            return Result.Fail(errorMessage);
         }

         return Result.Ok();
      }

      // Result<TValue>

      public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
      {
         return result ? func(result.Value) : Result.Fail(result.Error);
      }

      public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<Result<K>> func)
      {
         return result ? func() : Result.Fail<K>(result.Error);
      }

      public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
      {
         return result ? Result.Ok(func(result.Value)) : Result.Fail<K>(result.Error);
      }

      public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
      {
         return result ? func(result.Value) : Result.Fail<K>(result.Error);
      }

      public static Result<T> OnFailure<T>(this Result<T> result, Action action)
      {
         if (result.IsFail)
         {
            action();
         }

         return result;
      }

      public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
      {
         if (result.IsFail)
         {
            action(result.Error);
         }

         return result;
      }

      public static K OnBoth<T, K>(this Result<T> result, Func<Result<T>, K> func)
      {
         return func(result);
      }

      public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
      {
         if (result.IsFail)
         {
            return Result.Fail<T>(result.Error);
         }

         if (!predicate(result.Value))
         {
            return Result.Fail<T>(errorMessage);
         }

         return Result.Ok(result.Value);
      }

      // Result<TValue, TError>

      public static Result OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
         Func<TValue, Result> func) where TError : class
      {
         return result ? func(result.Value) : Result.Fail<TNewValue, TError>(result.Error);
      }

      public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
      {
         if (result)
         {
            action(result.Value);
         }

         return result;
      }

      public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
         Func<TValue, TNewValue> func) where TError : class
      {
         return result ? Result.Ok<TNewValue, TError>(func(result.Value)) : Result.Fail<TNewValue, TError>(result.Error);
      }

      public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
         Func<TValue, Result<TNewValue, TError>> func) where TError : class
      {
         return result.IsFail ? Result.Fail<TNewValue, TError>(result.Error) : func(result.Value);
      }

      public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
         Func<Result<TNewValue, TError>> func) where TError : class
      {
         return result ? func() : Result.Fail<TNewValue, TError>(result.Error);
      }

      public static Result<TNewValue> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
         Func<TValue, Result<TNewValue>> func) where TError : class
      {
         return result ? func(result.Value) : Result.Fail<TNewValue, TError>(result.Error);
      }

      public static Result<TValue, TError> OnSuccess<TValue, TError>(this Result<TValue, TError> result, Action<TValue> action) where TError : class
      {
         if (result)
         {
            action(result.Value);
         }

         return result;
      }

      public static Result<TValue, TError> OnFailure<TValue, TError>(this Result<TValue, TError> result,
         Action action) where TError : class
      {
         if (result.IsFail)
         {
            action();
         }

         return result;
      }

      public static Result<TValue, TError> OnFailure<TValue, TError>(this Result<TValue, TError> result,
         Action<TError> action) where TError : class
      {
         if (result.IsFail)
         {
            action(result.Error);
         }

         return result;
      }

      public static TValue OnBoth<TValue, TError>(this Result<TValue, TError> result,
         Func<Result<TValue, TError>, TValue> func) where TError : class
      {
         return func(result);
      }

      public static Result<TValue, TError> Ensure<TValue, TError>(this Result<TValue, TError> result,
         Func<TValue, bool> predicate, TError errorObject) where TError : class
      {
         if (result.IsFail)
         {
            return Result.Fail<TValue, TError>(result.Error);
         }

         if (!predicate(result.Value))
         {
            return Result.Fail<TValue, TError>(errorObject);
         }

         return Result.Ok<TValue, TError>(result.Value);
      }
   }
}

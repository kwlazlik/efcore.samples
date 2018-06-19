using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
   [Serializable]
   public struct Result
   {
      private readonly string _error;

      private Result(string error)
      {
         _error = string.IsNullOrEmpty(error) ? throw new ArgumentNullException(nameof(error)) : error;
      }

      public string Error => IsFail ? _error : throw new InvalidOperationException("Can not get error for ok result.");

      public bool IsOk => string.IsNullOrEmpty(_error);
      public bool IsFail => !IsOk;

      public static bool operator true(Result result) => result.IsOk;

      public static bool operator false(Result result) => result.IsFail;

      public static Result Ok() => new Result();

      public static Result Fail(string error) => new Result(error);

      public static ResultValue<TValue> Ok<TValue>(TValue value) => new ResultValue<TValue>(value);

      public static ResultValue<TValue> Fail<TValue>(string error) => new ResultValue<TValue>(default, error);

      public static ResultValue<TValue, TError> Ok<TValue, TError>(TValue value) where TError : class => new ResultValue<TValue, TError>(value);

      public static ResultValue<TValue, TError> Fail<TValue, TError>(TError error) where TError : class => new ResultValue<TValue, TError>(default, error);


      public static Result Combine(params Result[] results)
      {
         return Combine(", ", results);
      }

      public static Result Combine<T>(params ResultValue<T>[] results)
      {
         return Combine(", ", results);
      }

      public static Result Combine(string errorMessagesSeparator, params Result[] results)
      {
         List<Result> failedResults = results.Where(x => x.IsFail).ToList();

         if (!failedResults.Any())
            return Ok();

         string errorMessage = string.Join(errorMessagesSeparator, failedResults.Select(x => x.Error));
         return Fail(errorMessage);
      }

      public static Result Combine<T>(string errorMessagesSeparator, params ResultValue<T>[] results)
      {
         Result[] untyped = results.Select(result => (Result)result).ToArray();
         return Combine(errorMessagesSeparator, untyped);
      }
   }

   [Serializable]
   public struct ResultValue<TValue>
   {
      private readonly TValue _value;
      private readonly string _error;

      public TValue Value => IsOk ? _value : throw new InvalidOperationException("Can not get value for fail result.");

      public string Error => IsFail ? _error : throw new InvalidOperationException("Can not get error for ok result.");

      public bool IsOk => string.IsNullOrEmpty(_error);
      public bool IsFail => !IsOk;

      internal ResultValue(TValue value)
      {
         if (value == null)
         {
            throw new ArgumentNullException(nameof(value));
         }

         _value = value;
         _error = null;
      }

      internal ResultValue(TValue value, string error)
      {
         _value = value;
         _error = string.IsNullOrEmpty(error) ? throw new ArgumentNullException(nameof(error)) : error;
      }

      public static bool operator true(ResultValue<TValue> result) => result.IsOk;

      public static bool operator false(ResultValue<TValue> result) => result.IsFail;

      public static implicit operator Result(ResultValue<TValue> result) => result ? Result.Ok() : Result.Fail(result.Error);

      public static implicit operator ResultValue<TValue, string>(ResultValue<TValue> self) => self ? Result.Ok<TValue, string>(self.Value) : Result.Fail<TValue, string>(self.Error);
   }


   [Serializable]
   public struct ResultValue<TValue, TError> where TError : class
   {
      private readonly TValue _value;
      private readonly TError _error;

      public TValue Value => IsOk ? _value : throw new InvalidOperationException("Can not get value for fail result.");

      public TError Error => IsFail ? _error : throw new InvalidOperationException("Can not get error for ok result.");

      public bool IsOk => _error == null;
      public bool IsFail => !IsOk;

      internal ResultValue(TValue value)
      {
         if (value == null)
         {
            throw new ArgumentNullException(nameof(value));
         }

         _value = value;
         _error = null;
      }

      internal ResultValue(TValue value, TError error)
      {
         _value = value;
         _error = error ?? throw new ArgumentNullException(nameof(error));
      }

      public static bool operator true(ResultValue<TValue, TError> result) => result.IsOk;

      public static bool operator false(ResultValue<TValue, TError> result) => result.IsFail;

      public static implicit operator Result(ResultValue<TValue, TError> result)
      {
         return result ? Result.Ok() : Result.Fail(result.Error.ToString());
      }

      public static implicit operator ResultValue<TValue>(ResultValue<TValue, TError> result)
      {
         return result ? Result.Ok(result.Value) : Result.Fail<TValue>(result.Error.ToString());
      }
   }
}

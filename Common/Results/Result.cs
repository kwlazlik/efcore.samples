using System;

namespace Common.Results
{
   [Serializable]
   public struct Result
   {
      private readonly string _error;

      private Result(string error) => _error = string.IsNullOrEmpty(error) ? throw new ArgumentNullException(nameof(error)) : error;

      public string Error => IsFail ? _error : throw new InvalidOperationException("Can not get error for ok result.");

      public bool IsOk => string.IsNullOrEmpty(_error);

      public bool IsFail => !IsOk;

      public static bool operator true(Result result) => result.IsOk;

      public static bool operator false(Result result) => result.IsFail;

      public static Result Ok() => new Result();

      public static Result Fail(string error) => new Result(error);

      public static Result<TValue> Ok<TValue>(TValue value) => new Result<TValue>(value);

      public static Result<TValue> Fail<TValue>(string error) => new Result<TValue>(default, error);
   }

   [Serializable]
   public struct Result<TValue>
   {
      private readonly TValue _value;
      private readonly string _error;

      public TValue Value => IsOk ? _value : throw new InvalidOperationException("Can not get value for fail result.");

      public string Error => IsFail ? _error : throw new InvalidOperationException("Can not get error for ok result.");

      public bool IsOk => string.IsNullOrEmpty(_error);

      public bool IsFail => !IsOk;

      internal Result(TValue value)
      {
         _value = value;
         _error = null;
      }

      internal Result(TValue value, string error)
      {
         _value = value;
         _error = string.IsNullOrEmpty(error) ? throw new ArgumentNullException(nameof(error)) : error;
      }

      public static bool operator true(Result<TValue> result) => result.IsOk;

      public static bool operator false(Result<TValue> result) => result.IsFail;

      public static explicit operator Result(Result<TValue> result) => result ? Result.Ok() : Result.Fail(result.Error);

      public static implicit operator Result<TValue>(TValue val) => Result.Ok(val);

      public static explicit operator TValue(Result<TValue> result) => result.Value;
   }
}

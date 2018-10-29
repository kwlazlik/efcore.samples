using System;

// ReSharper disable InconsistentNaming

namespace Common.Results
{
   public static class ResultExtensions
   {
      // Result

      public static Result OnSuccess(this Result result, Func<Result> func) => result ? func() : result;

      public static Result<TA> OnSuccess<TA>(this Result result, Func<TA> func) => result ? func() : Result.Fail<TA>(result.Error);

      public static Result<TA> OnSuccess<TA>(this Result result, Func<Result<TA>> func) => result ? func() : Result.Fail<TA>(result.Error);

      public static Result OnSuccess(this Result result, Action action)
      {
         if (result)
         {
            action();
         }

         return result;
      }

      public static Result OnFailure(this Result result, Action<string> action)
      {
         if (result)
         {
            return result;
         }

         action(result.Error);

         return result;
      }

      // Result<TA>

      public static Result<TA> OnSuccess<TA>(this Result<TA> result, Action action)
      {
         if (result)
         {
            action();
         }

         return result;
      }

      public static Result<TA> OnSuccess<TA>(this Result<TA> result, Action<TA> action)
      {
         if (result)
         {
            action(result.Value);
         }

         return result;
      }

      public static Result<TA> OnFailure<TA>(this Result<TA> result, Action<string> action)
      {
         if (result)
         {
            return result;
         }

         action(result.Error);

         return result;
      }

      public static Result<TB> OnSuccess<TA, TB>(this Result<TA> result, Func<TA, TB> func) => result ? func(result.Value) : Result.Fail<TB>(result.Error);

      public static Result<TB> OnSuccess<TA, TB>(this Result<TA> result, Func<TA, Result<TB>> func) => result ? func(result.Value) : Result.Fail<TB>(result.Error);

      public static Result<(TA, TB)> OnSuccessZip<TA, TB>(this Result<TA> result, Func<TA, TB> func) => result
         ? (result.Value, func(result.Value))
         : Result.Fail<(TA, TB)>(result.Error);

      public static Result<(TA, TB)> OnSuccessZip<TA, TB>(this Result<TA> resultA, Func<TA, Result<TB>> func)
      {
         if (resultA)
         {
            Result<TB> resultB = func(resultA.Value);

            return resultB ? (resultA.Value, resultB.Value) : Result.Fail<(TA, TB)>(resultB.Error);
         }

         return Result.Fail<(TA, TB)>(resultA.Error);
      }

      // Result<(TA, TB)>

      public static Result<(TA, TB)> OnSuccess<TA, TB>(this Result<(TA, TB)> result, Action<TA, TB> action)
      {
         if (result)
         {
            action(result.Value.Item1, result.Value.Item2);
         }

         return result;
      }

      public static Result<(TA, TB, TC)> OnSuccessZip<TA, TB, TC>(this Result<(TA, TB)> result, Func<TA, TB, TC> func) => result
         ? (result.Value.Item1, result.Value.Item2, func(result.Value.Item1, result.Value.Item2))
         : Result.Fail<(TA, TB, TC)>(result.Error);

      public static Result<(TA, TB, TC)> OnSuccessZip<TA, TB, TC>(this Result<(TA, TB)> resultAB, Func<TA, TB, Result<TC>> func)
      {
         if (resultAB)
         {
            Result<TC> resultC = func(resultAB.Value.Item1, resultAB.Value.Item2);

            return resultC ? (resultAB.Value.Item1, resultAB.Value.Item2, resultC.Value) : Result.Fail<(TA, TB, TC)>(resultC.Error);
         }

         return Result.Fail<(TA, TB, TC)>(resultAB.Error);
      }

      public static Result<TC> OnSuccessUnzip<TA, TB, TC>(this Result<(TA, TB)> result, Func<TA, TB, TC> func) => result
         ? func(result.Value.Item1, result.Value.Item2)
         : Result.Fail<TC>(result.Error);

      public static Result<TC> OnSuccessUnzip<TA, TB, TC>(this Result<(TA, TB)> result, Func<TA, TB, Result<TC>> func) => result
         ? func(result.Value.Item1, result.Value.Item2)
         : Result.Fail<TC>(result.Error);

      // Result<(TA, TB, TC)>

      public static Result<(TA, TB, TC)> OnSuccess<TA, TB, TC>(this Result<(TA, TB, TC)> result, Action<TA, TB, TC> action)
      {
         if (result)
         {
            action(result.Value.Item1, result.Value.Item2, result.Value.Item3);
         }

         return result;
      }

      public static Result<(TA, TB, TC, TD)> OnSuccessZip<TA, TB, TC, TD>(this Result<(TA, TB, TC)> result, Func<TA, TB, TC, TD> func) => result
         ? (result.Value.Item1, result.Value.Item2, result.Value.Item3, func(result.Value.Item1, result.Value.Item2, result.Value.Item3))
         : Result.Fail<(TA, TB, TC, TD)>(result.Error);

      public static Result<(TA, TB, TC, TD)> OnSuccessZip<TA, TB, TC, TD>(this Result<(TA, TB, TC)> resultABC, Func<TA, TB, TC, Result<TD>> func)
      {
         if (resultABC)
         {
            Result<TD> resultD = func(resultABC.Value.Item1, resultABC.Value.Item2, resultABC.Value.Item3);

            return resultABC ? (resultABC.Value.Item1, resultABC.Value.Item2, resultABC.Value.Item3, resultD.Value) : Result.Fail<(TA, TB, TC, TD)>(resultABC.Error);
         }

         return Result.Fail<(TA, TB, TC, TD)>(resultABC.Error);
      }

      public static Result<TD> OnSuccessUnzip<TA, TB, TC, TD>(this Result<(TA, TB, TC)> result, Func<TA, TB, TC, TD> func) => result
         ? func(result.Value.Item1, result.Value.Item2, result.Value.Item3)
         : Result.Fail<TD>(result.Error);

      public static Result<TD> OnSuccessUnzip<TA, TB, TC, TD>(this Result<(TA, TB, TC)> result, Func<TA, TB, TC, Result<TD>> func) => result
         ? func(result.Value.Item1, result.Value.Item2, result.Value.Item3)
         : Result.Fail<TD>(result.Error);
   }
}

using System.Linq;

namespace EFC.Interception
{
   public static class QueryableExtensions
   {
      public static IQueryable<T> FixExpression<T>(this IQueryable<T> queryable) => new InterceptingProvider(queryable.Provider, new FixExpressionVisitor()).CreateQuery<T>(queryable.Expression);
   }
}

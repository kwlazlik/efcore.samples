using System.Linq;
using System.Linq.Expressions;

namespace EFC.Interception
{
   public static class QueryableExtensions
   {
      public static IQueryable<T> FixExpression<T>(this IQueryable<T> queryable) => new InterceptingProvider(queryable.Provider, new FixExpressionVisitor()).CreateQuery<T>(queryable.Expression);

      public static IQueryable<T> FixExpression<T>(this IQueryable<T> queryable, params ExpressionVisitor[] visitors) => new InterceptingProvider(queryable.Provider, visitors).CreateQuery<T>(queryable.Expression);
   }
}

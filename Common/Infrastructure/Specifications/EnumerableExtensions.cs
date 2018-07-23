using System.Linq;

namespace Common.Infrastructure.Specifications
{
   public static class EnumerableExtensions
   {
      public static IQueryable<T> Where<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.Where(specification.ToExpression());
      }

      public static T First<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.First(specification.ToExpression());
      }

      public static T FirstOrDefault<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.FirstOrDefault(specification.ToExpression());
      }

      public static T Last<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.Last(specification.ToExpression());
      }

      public static T LastOrDefault<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.LastOrDefault(specification.ToExpression());
      }

      public static T Single<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.Single(specification.ToExpression());
      }

      public static T SingleOrDefault<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.SingleOrDefault(specification.ToExpression());
      }

      public static bool Any<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.Any(specification.ToExpression());
      }

      public static bool All<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.All(specification.ToExpression());
      }

      public static int Count<T>(this IQueryable<T> queryable, Specification<T> specification)
      {
         return queryable.Count(specification.ToExpression());
      }
   }
}

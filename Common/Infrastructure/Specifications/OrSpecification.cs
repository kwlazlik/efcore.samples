using System;
using System.Linq.Expressions;

namespace Common.Infrastructure.Specifications
{
   internal class OrSpecification<T> : Specification<T>
   {
      private readonly Specification<T> _left;
      private readonly Specification<T> _right;

      public OrSpecification(Specification<T> left, Specification<T> right)
      {
         _right = right;
         _left = left;
      }

      public override Expression<Func<T, bool>> ToExpression()
      {
         Expression<Func<T, bool>> leftExpression = _left.ToExpression();
         Expression<Func<T, bool>> rightExpression = _right.ToExpression();
         ParameterExpression paramExpr = Expression.Parameter(typeof(T));
         BinaryExpression exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
         exprBody = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(exprBody);
         Expression<Func<T, bool>> finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

         return finalExpr;
      }
   }
}
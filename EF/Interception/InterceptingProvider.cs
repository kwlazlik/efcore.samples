using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFC.Interception
{
   public class InterceptingProvider : IQueryProvider
   {
      private readonly IQueryProvider _inner;
      private readonly ExpressionVisitor[] _visitors;

      public InterceptingProvider(IQueryProvider inner, params ExpressionVisitor[] visitors)
      {
         _inner = inner;
         _visitors = visitors;
      }

      public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
         new InterceptedQueryable<TElement>(this, _inner.CreateQuery<TElement>(InterceptExpresion(expression)));

      public IQueryable CreateQuery(Expression expression) => new InterceptedQueryable(this, _inner.CreateQuery(InterceptExpresion(expression)));

      public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(InterceptExpresion(expression));

      public object Execute(Expression expression) => _inner.Execute(InterceptExpresion(expression));

      private Expression InterceptExpresion(Expression expression) => _visitors
        .Select<ExpressionVisitor, Func<Expression, Expression>>(v => v.Visit)
        .Aggregate(expression, (current, visitor) => visitor(current));
   }
}

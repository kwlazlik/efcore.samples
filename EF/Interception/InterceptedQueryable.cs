using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFC.Interception
{

   public sealed class InterceptedQueryable : IOrderedQueryable
   {
      private readonly IQueryable _inner;

      public InterceptedQueryable(IQueryProvider provider, IQueryable inner)
      {
         _inner = inner;
         Provider = provider;
      }

      public Expression Expression => _inner.Expression;

      public Type ElementType => _inner.ElementType;

      public IQueryProvider Provider { get; }

      public IEnumerator GetEnumerator() => _inner.GetEnumerator();

      public override string ToString() => _inner.ToString();
   }

   public sealed class InterceptedQueryable<T> : IOrderedQueryable<T>
   {
      private readonly IQueryable<T> _inner;

      public InterceptedQueryable(IQueryProvider provider, IQueryable<T> inner)
      {
         _inner = inner;
         Provider = provider;
      }

      public Expression Expression => _inner.Expression;

      public Type ElementType => _inner.ElementType;

      public IQueryProvider Provider { get; }

      IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();

      public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();

      public override string ToString() => _inner.ToString();
   }
}

// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IQToolkit
{
   /// <summary>
   ///    Optional interface for <see cref="IQueryProvider" /> to implement <see cref="Query{T}.QueryText" /> property.
   /// </summary>
   public interface IQueryText
   {
      string GetQueryText(Expression expression);
   }

   /// <summary>
   ///    A default implementation of IQueryable for use with QueryProvider
   /// </summary>
   public class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
   {
      public Query(IQueryProvider provider)
         : this(provider, null) { }

      public Query(IQueryProvider provider, Type staticType)
      {
         if (provider == null) throw new ArgumentNullException("Provider");
         Provider = provider;
         Expression = staticType != null ? Expression.Constant(this, staticType) : Expression.Constant(this);
      }

      public Query(QueryProvider provider, Expression expression)
      {
         if (provider == null) throw new ArgumentNullException("Provider");
         if (expression == null) throw new ArgumentNullException("expression");
         if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type)) throw new ArgumentOutOfRangeException("expression");
         Provider = provider;
         Expression = expression;
      }

      public string QueryText
      {
         get
         {
            var iqt = Provider as IQueryText;
            if (iqt != null) return iqt.GetQueryText(Expression);
            return "";
         }
      }

      public Expression Expression { get; }

      public Type ElementType => typeof(T);

      public IQueryProvider Provider { get; }

      public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>) Provider.Execute(Expression)).GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Provider.Execute(Expression)).GetEnumerator();

      public override string ToString()
      {
         if (Expression.NodeType == ExpressionType.Constant &&
             ((ConstantExpression) Expression).Value == this)
            return "Query(" + typeof(T) + ")";
         return Expression.ToString();
      }
   }
}

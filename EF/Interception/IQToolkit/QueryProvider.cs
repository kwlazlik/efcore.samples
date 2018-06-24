// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace IQToolkit
{
   /// <summary>
   ///    A basic abstract LINQ query provider
   /// </summary>
   public abstract class QueryProvider : IQueryProvider, IQueryText
   {
      IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression) => new Query<S>(this, expression);

      IQueryable IQueryProvider.CreateQuery(Expression expression)
      {
         Type elementType = TypeHelper.GetElementType(expression.Type);
         try
         {
            return (IQueryable) Activator.CreateInstance(typeof(Query<>).MakeGenericType(elementType), this, expression);
         }
         catch (TargetInvocationException tie)
         {
            throw tie.InnerException;
         }
      }

      S IQueryProvider.Execute<S>(Expression expression) => (S) Execute(expression);

      object IQueryProvider.Execute(Expression expression) => Execute(expression);

      public abstract string GetQueryText(Expression expression);
      public abstract object Execute(Expression expression);
   }
}

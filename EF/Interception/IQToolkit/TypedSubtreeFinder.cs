// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Linq.Expressions;

namespace IQToolkit
{
   /// <summary>
   ///    Finds the first sub-expression that is of a specified type
   /// </summary>
   public class TypedSubtreeFinder : ExpressionVisitor
   {
      private Expression root;
      private readonly Type type;

      private TypedSubtreeFinder(Type type) => this.type = type;

      public static Expression Find(Expression expression, Type type)
      {
         var finder = new TypedSubtreeFinder(type);
         finder.Visit(expression);
         return finder.root;
      }

      protected override Expression Visit(Expression exp)
      {
         Expression result = base.Visit(exp);

         // remember the first sub-expression that produces an IQueryable
         if (root == null && result != null)
            if (type.IsAssignableFrom(result.Type))
               root = result;

         return result;
      }
   }
}

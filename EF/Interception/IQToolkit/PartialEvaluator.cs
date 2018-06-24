﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IQToolkit
{
   /// <summary>
   ///    Rewrites an expression tree so that locally isolatable sub-expressions are evaluated and converted into ConstantExpression nodes.
   /// </summary>
   public static class PartialEvaluator
   {
      /// <summary>
      ///    Performs evaluation and replacement of independent sub-trees
      /// </summary>
      /// <param name="expression">The root of the expression tree.</param>
      /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
      public static Expression Eval(Expression expression) => Eval(expression, null, null);

      /// <summary>
      ///    Performs evaluation and replacement of independent sub-trees
      /// </summary>
      /// <param name="expression">The root of the expression tree.</param>
      /// <param name="fnCanBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
      /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
      public static Expression Eval(Expression expression, Func<Expression, bool> fnCanBeEvaluated) => Eval(expression, fnCanBeEvaluated, null);

      public static Expression Eval(Expression expression, Func<Expression, bool> fnCanBeEvaluated, Func<ConstantExpression, Expression> fnPostEval)
      {
         if (fnCanBeEvaluated == null)
            fnCanBeEvaluated = CanBeEvaluatedLocally;
         return SubtreeEvaluator.Eval(Nominator.Nominate(fnCanBeEvaluated, expression), fnPostEval, expression);
      }

      private static bool CanBeEvaluatedLocally(Expression expression) => expression.NodeType != ExpressionType.Parameter;

      /// <summary>
      ///    Evaluates and replaces sub-trees when first candidate is reached (top-down)
      /// </summary>
      private class SubtreeEvaluator : ExpressionVisitor
      {
         private readonly HashSet<Expression> candidates;
         private readonly Func<ConstantExpression, Expression> onEval;

         private SubtreeEvaluator(HashSet<Expression> candidates, Func<ConstantExpression, Expression> onEval)
         {
            this.candidates = candidates;
            this.onEval = onEval;
         }

         internal static Expression Eval(HashSet<Expression> candidates, Func<ConstantExpression, Expression> onEval, Expression exp) =>
            new SubtreeEvaluator(candidates, onEval).Visit(exp);

         protected override Expression Visit(Expression exp)
         {
            if (exp == null) return null;
            if (candidates.Contains(exp)) return Evaluate(exp);
            return base.Visit(exp);
         }

         private Expression PostEval(ConstantExpression e)
         {
            if (onEval != null) return onEval(e);
            return e;
         }

         private Expression Evaluate(Expression e)
         {
            Type type = e.Type;
            if (e.NodeType == ExpressionType.Convert)
            {
               // check for unnecessary convert & strip them
               var u = (UnaryExpression) e;
               if (TypeHelper.GetNonNullableType(u.Operand.Type) == TypeHelper.GetNonNullableType(type)) e = ((UnaryExpression) e).Operand;
            }

            if (e.NodeType == ExpressionType.Constant)
            {
               // in case we actually threw out a nullable conversion above, simulate it here
               // don't post-eval nodes that were already constants
               if (e.Type == type)
                  return e;
               if (TypeHelper.GetNonNullableType(e.Type) == TypeHelper.GetNonNullableType(type))
                  return Expression.Constant(((ConstantExpression) e).Value, type);
            }

            var me = e as MemberExpression;
            if (me != null)
            {
               // member accesses off of constant's are common, and yet since these partial evals
               // are never re-used, using reflection to access the member is faster than compiling  
               // and invoking a lambda
               var ce = me.Expression as ConstantExpression;
               if (ce != null) return PostEval(Expression.Constant(me.Member.GetValue(ce.Value), type));
            }

            if (type.IsValueType) e = Expression.Convert(e, typeof(object));
            Expression<Func<object>> lambda = Expression.Lambda<Func<object>>(e);
            #if NOREFEMIT
                Func<object> fn = ExpressionEvaluator.CreateDelegate(lambda);
#else
            Func<object> fn = lambda.Compile();
            #endif
            return PostEval(Expression.Constant(fn(), type));
         }
      }

      /// <summary>
      ///    Performs bottom-up analysis to determine which nodes can possibly
      ///    be part of an evaluated sub-tree.
      /// </summary>
      private class Nominator : ExpressionVisitor
      {
         private readonly HashSet<Expression> candidates;
         private bool cannotBeEvaluated;
         private readonly Func<Expression, bool> fnCanBeEvaluated;

         private Nominator(Func<Expression, bool> fnCanBeEvaluated)
         {
            candidates = new HashSet<Expression>();
            this.fnCanBeEvaluated = fnCanBeEvaluated;
         }

         internal static HashSet<Expression> Nominate(Func<Expression, bool> fnCanBeEvaluated, Expression expression)
         {
            var nominator = new Nominator(fnCanBeEvaluated);
            nominator.Visit(expression);
            return nominator.candidates;
         }

         protected override Expression VisitConstant(ConstantExpression c) => base.VisitConstant(c);

         protected override Expression Visit(Expression expression)
         {
            if (expression != null)
            {
               bool saveCannotBeEvaluated = cannotBeEvaluated;
               cannotBeEvaluated = false;
               base.Visit(expression);
               if (!cannotBeEvaluated)
               {
                  if (fnCanBeEvaluated(expression))
                     candidates.Add(expression);
                  else
                     cannotBeEvaluated = true;
               }

               cannotBeEvaluated |= saveCannotBeEvaluated;
            }

            return expression;
         }
      }
   }
}

using System;
using System.Linq.Expressions;
using Common.Infrastructure.Specifications;

namespace TestProject.Core.SpecificationTest
{
   public class TestSpecification : Specification<HasTest>
   {
      private readonly Expression<Func<HasTest, bool>> _expression;

      public TestSpecification(string[] props)
      {
         ParameterExpression parameterExpression = Expression.Parameter(typeof(HasTest), "t");

         Expression orsExpression = Expression.Property(Expression.Property(parameterExpression, "Test"), props[0]);

         for (int i = 1; i < props.Length; i++)
         {
            orsExpression = Expression.OrElse(orsExpression, Expression.Property(Expression.Property(parameterExpression, "Test"), props[i]));
         }

         _expression = Expression.Lambda<Func<HasTest, bool>>(orsExpression, parameterExpression);
      }

      public override Expression<Func<HasTest, bool>> ToExpression() => _expression;
   }
}

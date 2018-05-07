using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp2.Domain
{
   public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
   {
      private static readonly Func<T, T, bool> EqualsFunc = BuildEqualsMethod();
      private static readonly Func<T, int> GetHashCodeFunc = BuildGetHashCodeMethod();

      public bool Equals(T other) => ReferenceEquals(this, other) || !(other is null) && EqualsFunc(this as T, other);

      public static bool operator ==(ValueObject<T> left, T right) => ReferenceEquals(left, right) || !(left is null) && !(right is null) && EqualsFunc(left as T, right);

      public static bool operator !=(ValueObject<T> left, T right) => !(left == right);

      public override bool Equals(object obj) => Equals(obj as T);

      public override int GetHashCode() => GetHashCodeFunc(this as T);

      private static Func<T, int> BuildGetHashCodeMethod()
      {
         PropertyInfo[] properties = typeof(T).GetTypeInfo().GetProperties();

         ParameterExpression paramExpression = Expression.Parameter(typeof(T), "sefl");

         IEnumerable<Expression> hashCodeExpressions = properties.Where(pi => pi.GetIndexParameters().Length == 0).Select(pi =>
         {
            Type propertyType = pi.PropertyType;
            Expression propertyExpression = Expression.Property(paramExpression, pi);
            MethodInfo getHashCode = propertyType.GetTypeInfo().GetMethod("GetHashCode");

            return propertyType.GetTypeInfo().IsValueType
               ? (Expression)Expression.Call(propertyExpression, getHashCode)
               : Expression.Condition(Expression.Equal(propertyExpression, Expression.Constant(null, propertyType)),
                                      Expression.Constant(0, typeof(int)), Expression.Call(propertyExpression, getHashCode));
         });

         Expression constExpression = Expression.Constant(127, typeof(int));
         Expression xorExpression = hashCodeExpressions.Aggregate(constExpression, Expression.ExclusiveOr);

         return Expression.Lambda<Func<T, int>>(xorExpression, paramExpression).Compile();
      }

      private static Func<T, T, bool> BuildEqualsMethod()
      {
         PropertyInfo[] properties = typeof(T).GetTypeInfo().GetProperties();

         ParameterExpression leftParam = Expression.Parameter(typeof(T), "left");
         ParameterExpression rightParam = Expression.Parameter(typeof(T), "right");

         IEnumerable<Expression> equalsExpressions = properties.Where(pi => pi.GetIndexParameters().Length == 0).Select(pi =>
         {
            MemberExpression leftProperty = Expression.Property(leftParam, pi);
            MemberExpression rightProperty = Expression.Property(rightParam, pi);

            Type propertyType = pi.PropertyType;
            Type genericEquatableType = typeof(IEquatable<>).MakeGenericType(propertyType);
            MethodInfo equalsMethodInfo;

            if (propertyType.GetTypeInfo().IsValueType && genericEquatableType.GetTypeInfo().IsAssignableFrom(propertyType))
            {
               equalsMethodInfo = propertyType.GetTypeInfo().GetMethod("Equals", new[] { propertyType });
               return Expression.Call(leftProperty, equalsMethodInfo, rightProperty);
            }

            equalsMethodInfo = propertyType.GetTypeInfo().GetMethod("Equals", BindingFlags.Public | BindingFlags.Static);
            if (equalsMethodInfo != null)
            {
               return Expression.Call(null, equalsMethodInfo, leftProperty, rightProperty);
            }

            equalsMethodInfo = typeof(object).GetTypeInfo().GetMethod("Equals", BindingFlags.Public | BindingFlags.Static);
            UnaryExpression convertedLeftProperty = Expression.Convert(leftProperty, typeof(object));
            UnaryExpression convertedRightProperty = Expression.Convert(rightProperty, typeof(object));

            return Expression.Call(null, equalsMethodInfo, convertedLeftProperty, convertedRightProperty);
         });

         Expression trueExpression = Expression.Constant(true, typeof(bool));
         Expression andExpression = equalsExpressions.Aggregate(trueExpression, Expression.And);

         return Expression.Lambda<Func<T, T, bool>>(andExpression, leftParam, rightParam).Compile();
      }
   }
}

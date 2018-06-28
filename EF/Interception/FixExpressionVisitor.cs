using System.Linq.Expressions;
using Domain;

namespace EFC.Interception
{
   public class FixExpressionVisitor : ExpressionVisitor
   {
      protected override Expression VisitBinary(BinaryExpression node)
      {
         BinaryExpression newNode = node;
         Expression left = node.Left;
         Expression right = node.Right;
         bool newNodeNeeded = false;

         if (typeof(ISingleValueValueType<object>).IsAssignableFrom(node.Left.Type))
         {
            left = Expression.Property(node.Left, node.Left.Type, nameof(ISingleValueValueType<object>.Value));
            newNodeNeeded = true;
         }

         if (typeof(ISingleValueValueType<object>).IsAssignableFrom(node.Right.Type))
         {
            right = Expression.Property(node.Right, node.Left.Type, nameof(ISingleValueValueType<object>.Value));
            newNodeNeeded = true;
         }

         if (newNodeNeeded)
         {
            newNode = Expression.MakeBinary(ExpressionType.Equal, left, right);
         }

         return base.VisitBinary(newNode);
      }
   }
}

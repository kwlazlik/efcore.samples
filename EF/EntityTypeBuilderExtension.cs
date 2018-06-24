using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFC
{
   public static class EntityTypeBuilderExtension
   {
      public static PropertyBuilder OwnsEnumeration<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> builder,
                                                                             Expression<Func<TEntity, TRelatedEntity>> navigationExpression)
         where TRelatedEntity : class where TEntity : class
      {
         ReferenceOwnershipBuilder<TEntity, TRelatedEntity> referenceOwnershipBuilder = builder.OwnsOne(navigationExpression);
         return referenceOwnershipBuilder.Property("Value").HasColumnName(referenceOwnershipBuilder.Metadata.PrincipalToDependent.Name);
      }
   }
}

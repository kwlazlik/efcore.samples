using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq;

namespace EFC
{
   public static class IQueryableHelper
   {
      private static readonly FieldInfo _queryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.Single(x => x.Name == "_queryCompiler");

      private static readonly TypeInfo _queryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

      private static readonly FieldInfo _queryModelGeneratorField = _queryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_queryModelGenerator");

      private static readonly FieldInfo _databaseField = _queryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

      private static readonly PropertyInfo _dependenciesProperty = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

      public static string ToSql<TEntity>(this IQueryable<TEntity> queryable)
         where TEntity : class
      {
         if (!(queryable is EntityQueryable<TEntity>) && !(queryable is InternalDbSet<TEntity>))
            throw new ArgumentException();

         IQueryCompiler queryCompiler = (IQueryCompiler)_queryCompilerField.GetValue(queryable.Provider);
         IQueryModelGenerator queryModelGenerator = (IQueryModelGenerator)_queryModelGeneratorField.GetValue(queryCompiler);
         QueryModel queryModel = queryModelGenerator.ParseQuery(queryable.Expression);
         object database = _databaseField.GetValue(queryCompiler);
         IQueryCompilationContextFactory queryCompilationContextFactory = ((DatabaseDependencies)_dependenciesProperty.GetValue(database)).QueryCompilationContextFactory;
         QueryCompilationContext queryCompilationContext = queryCompilationContextFactory.Create(false);
         RelationalQueryModelVisitor modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
         modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
         return modelVisitor.Queries.Join(Environment.NewLine + Environment.NewLine);
      }
   }
}

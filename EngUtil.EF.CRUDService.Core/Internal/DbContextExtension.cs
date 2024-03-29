// --------------------------------------------------------------------------------
// <copyright filename="DbContextExtension.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EngUtil.EF.CRUDService.Core
{
    internal static class DbContextExtension
    {
        public static IQueryable<T> GetDbSetAsIQuariable<T>(this DbContext dbContext)
        {
            return (IQueryable<T>)dbContext.GetDbSetAsIQuariable(typeof(T));
        }

        public static IQueryable GetDbSetAsIQuariable(this DbContext dbContext, Type entityType)
        {
            return (IQueryable)GetGenericSetMethodFromDbContext(entityType).Invoke(dbContext, null);
        }

        public static object DbSetAdd(this DbContext dbContext, object entry)
        {
            var dbSet = typeof(DbContext)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name == nameof(DbContext.Set) && x.IsGenericMethod == true)
                .MakeGenericMethod(entry.GetType()).Invoke(dbContext, null);
            object returnEntry = dbSet.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name == nameof(DbSet<object>.Add) && x.IsGenericMethod != true)
                .Invoke(dbSet, new[] { entry });
            return ((EntityEntry)returnEntry).Entity;
        }

        public static IQueryable<TResult> BuildQuery<TSource, TResult>(this DbContext dbContext, Expression<Func<TSource, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0)
        {
            var queryableSet = dbContext.GetDbSetAsIQuariable<TSource>();
            var query = queryableSet.Select(selector);
            if (queryableSet == null)
                throw new NullReferenceException($"Could not found DbSet of Entity-Type {typeof(TSource).Name} in DbContext!");
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
            {
                query = orderBy(query);

                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);
            }
            return query;
        }

        private static MethodInfo GetGenericSetMethodFromDbContext(Type genericType)
        {
            return typeof(DbContext)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name == nameof(DbContext.Set) && x.IsGenericMethod == true)
                .MakeGenericMethod(genericType);
        }
    }
}

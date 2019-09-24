using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace engUtil.EF.CRUDService.Core.Helper
{
    public static class DbContextExtension
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

        private static MethodInfo GetGenericSetMethodFromDbContext(Type genericType)
        {
            return typeof(DbContext)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name == nameof(DbContext.Set) && x.IsGenericMethod == true)
                .MakeGenericMethod(genericType);
        }
    }
}

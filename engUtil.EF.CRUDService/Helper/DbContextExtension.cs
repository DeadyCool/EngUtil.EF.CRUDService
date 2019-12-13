// --------------------------------------------------------------------------------
// <copyright filename="DbContextExtension.cs" date="12-13-2019">
// (c) 2019 All Rights Reserved
// </copyright>
// <author>
// Oliver Engels
// </author>
// --------------------------------------------------------------------------------
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace engUtil.EF.CRUDService.Helper
{
    public static class DbContextExtension
    {
        public static IQueryable<T> GetDbSetAsIQuariable<T>(this DbContext dbContext)
        {
            return (IQueryable<T>)dbContext.GetDbSetAsIQuariable(typeof(T));
        }

        public static IQueryable  GetDbSetAsIQuariable(this DbContext dbContext, Type entityType)
        {
            return (IQueryable)GetGenericSetMethodFromDbContext(entityType).Invoke(dbContext, null);
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

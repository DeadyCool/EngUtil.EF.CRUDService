// --------------------------------------------------------------------------------
// <copyright filename="IDbSetAccessor.cs" date="20-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core.Interfaces
{
    /// <summary>
    /// A interface to allows access to the db context from the repository
    /// </summary>
    /// <typeparam name="TSet">Represents the entity type from a <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/></typeparam>
    public interface IDbSetAccessor<TSet>
    {
        /// <summary>
        /// Allows to filter and select an <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> from a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entity
        /// </summary>
        /// <typeparam name="TResult">A type that represents the resultset</typeparam>
        /// <param name="selector">Represents a select from a <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> entity</param>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <returns></returns>
        IEnumerable<TResult> Select<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Allows to filter and select an <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> from a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entity
        /// </summary>
        /// <typeparam name="TResult">A type that represents the resultset</typeparam>
        /// <param name="selector">Represents a select from a <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> entity</param>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <returns></returns>
        Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Allows to filter and distinct select an <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> from a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entity
        /// </summary>
        /// <typeparam name="TResult">A type that represents the resultset</typeparam>
        /// <param name="filter">Specifies a filter expression</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <returns></returns>
        IEnumerable<TResult> Distinct<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Allows to filter and distinct select an <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> from a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entity
        /// </summary>
        /// <typeparam name="TResult">A type that represents the resultset</typeparam>
        /// <param name="selector">Represents a select from a <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> entity</param>
        /// <param name="filter">Specifies a filter expression</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <returns></returns>
        Task<IEnumerable<TResult>> DistinctAsync<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Allows to count and filter an <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> from a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entity
        /// </summary>
        /// <typeparam name="TResult">A type that represents the resultset that will be count</typeparam>
        /// <param name="selector">Represents a select from a <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> entity</param>
        /// <param name="filter">Specifies a filter expression</param>
        /// <returns></returns>
        int Count<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null);

        /// <summary>
        /// Allows to count and filter an <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> from a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entity
        /// </summary>
        /// <typeparam name="TResult">A type that represents the resultset that will be count</typeparam>
        /// <param name="selector">Represents a select from a <see cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}Set"/> entity</param>
        /// <param name="filter">Specifies a filter expression</param>
        /// <returns></returns>
        Task<int> CountAsync<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null);
    }
}

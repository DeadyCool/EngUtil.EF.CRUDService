// --------------------------------------------------------------------------------
// <copyright filename="DbSetSelector.cs" date="25-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// A instance to get access to a <see cref="Microsoft.EntityFrameworkCore.DbContext"/
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    public class DbSetSelector<TSet> : IDbSetSelector<TSet>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDbContextBuilder _dbContextAccessor;

        /// <summary>
        /// Creates a instance of a DbSet accsessor with given parameters
        /// </summary>
        /// <param name="dbContextAccessor">Represents a interface to access a <see cref="Microsoft.EntityFrameworkCore.DbContext"/></param>
        public DbSetSelector(IDbContextBuilder dbContextAccessor)
        {
            _dbContextAccessor = dbContextAccessor;
        }

        /// <inheritdoc/>
        public IEnumerable<TResult> Select<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var context = _dbContextAccessor.CreateContext())
            {
                var query = context.BuildQuery(selector, filter, orderBy, skip, take);
                return query.ToList();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var context = _dbContextAccessor.CreateContext())
            {
                var query = context.BuildQuery(selector, filter, orderBy, skip, take);
                return await query.ToListAsync();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TResult> Distinct<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var context = _dbContextAccessor.CreateContext())
            {
                var query = context.BuildQuery(selector, filter, orderBy, skip, take).Distinct();
                return query.ToList();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> DistinctAsync<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var context = _dbContextAccessor.CreateContext())
            {
                var query = context.BuildQuery(selector, filter, orderBy, skip, take).Distinct();
                return await query.ToArrayAsync();
            }
        }

        /// <inheritdoc/>
        public int Count<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null)
        {
            using (var context = _dbContextAccessor.CreateContext())
            {
                var query = context.BuildQuery(selector, filter);
                return query.Count();
            }
        }

        /// <inheritdoc/>
        public async Task<int> CountAsync<TResult>(Expression<Func<TSet, TResult>> selector, Expression<Func<TResult, bool>> filter = null)
        {
            using (var context = _dbContextAccessor.CreateContext())
            {
                var query = context.BuildQuery(selector, filter);
                return await query.CountAsync();
            }
        }
    }
}

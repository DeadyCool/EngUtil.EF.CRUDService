// --------------------------------------------------------------------------------
// <copyright filename="Repository.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// A abstract class with CRUD-Operations for Microsoft.EntityFrameworkCore
    /// </summary>
    /// <typeparam name="TEntity">Represents a <see cref="DbSet{TEntity}"/></typeparam>
    /// <typeparam name="TModel">Represents a Dto-Model for a <see cref="DbSet{TEntity}"/>y</typeparam>
    public abstract class ReadOnlyRepository<TEntity, TModel> : IReadOnlyRepository<TModel>, IRepositoryReadDto<TEntity, TModel>
            where TEntity : class
            where TModel : class
    {

        #region fields

        private DbContextOptions _dbContextOptions;

        #endregion

        #region ctor    

        /// <summary>
        /// Creates a instance with given parameter
        /// </summary>
        /// <param name="contextOptions">Represents the <see cref="DbContextOptions"/> with a specific <see cref="DbContext"/> to get access to the DbSets</param>
        protected ReadOnlyRepository(DbContextOptions contextOptions)      
        {
            _dbContextOptions = contextOptions;
        }

        #endregion

        #region properties
        /// <inheritdoc/>
        public virtual Expression<Func<TEntity, TModel>> AsModelExpression { get; set; }

        #endregion

        #region methods

        /// <inheritdoc/>
        public virtual TModel AsModel(TEntity entity)
        {
            return AsModelExpression.Compile().Invoke(entity);
        }

        /// <inheritdoc/>
        public virtual int Count(Expression<Func<TModel, bool>> filter = null)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter);
                return query.Count();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(Expression<Func<TModel, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter);
                return await query.CountAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TModel> Distinct(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var context = CreateContext())
            {
                var query = context
                    .BuildQuery(AsModelExpression, filter)
                    .Distinct();
                return query.ToList();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TModel>> DistinctAsync(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var query = context
                    .BuildQuery(AsModelExpression, filter)
                    .Distinct();
                return await query.ToListAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter, orderBy, skip, take);
                return query.ToList();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter, orderBy, skip, take);
                return await query.ToListAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public virtual TModel GetFirst(Expression<Func<TModel, bool>> filter)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter);
                return query.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter);
                return await query.FirstOrDefaultAsync(cancellationToken);
            }
        }

        #region methods: private

        internal DbContext CreateContext()
        {
            if (_dbContextOptions != null)
                return (DbContext)Activator.CreateInstance(_dbContextOptions.ContextType, _dbContextOptions);
            throw new Exception("DbContextOptions missing");
        }

        #endregion

        #endregion
    }
}
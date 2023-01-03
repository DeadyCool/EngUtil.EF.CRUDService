// --------------------------------------------------------------------------------
// <copyright filename="Repository.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// A abstract class with CRUD-Operations for Microsoft.EntityFrameworkCore <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext">Represents the o type of <see cref="DbContext"/></typeparam>
    /// <typeparam name="TEntity">Represents a <see cref="DbSet{TEntity}"/></typeparam>
    /// <typeparam name="TModel">Represents a Dto-Model for a <see cref="DbSet{TEntity}"/>y</typeparam>
    public abstract class Repository<TDbContext, TEntity, TModel> : DbContextBuilder<TDbContext>, IRepository<TModel>, IRepositoryDto<TEntity, TModel>
            where TDbContext : DbContext
            where TEntity : class
            where TModel : class
    {

        #region ctor    

        /// <summary>
        /// Creates a instance with given parameter
        /// </summary>
        /// <param name="contextOptions">Represents the <see cref="DbContextOptions"/> with a specific <see cref="DbContext"/> to get access to the DbSets</param>
        protected Repository(DbContextOptions<TDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        #endregion

        #region properties

        /// <inheritdoc/>
        public abstract Expression<Func<TModel, TEntity>> AsEntityExpression { get; }

        /// <inheritdoc/>
        public abstract Expression<Func<TEntity, TModel>> AsModelExpression { get; }

        #endregion

        #region methods

        /// <inheritdoc/>
        public virtual TEntity AsEntity(TModel model)
        {
            return AsEntityExpression.Compile().Invoke(model);
        }

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
        public virtual async Task<int> CountAsync(Expression<Func<TModel, bool>> filter = null)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter);
                return await query.CountAsync();
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
        public virtual async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var hndl = new QueryHandler<TDbContext>())
            {
                var query = hndl.ToQuery(this, AsModelExpression, filter, orderBy, skip, take);
                return await query.ToListAsync();
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
        public virtual async Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> filter)
        {
            using (var context = CreateContext())
            {
                var query = context.BuildQuery(AsModelExpression, filter);
                return await query.FirstOrDefaultAsync();
            }
        }

        /// <inheritdoc/>
        public virtual TModel Insert(TModel model)
        {
            using (var context = CreateContext())
            {
                var entity = context.DbSetAdd(AsEntity(model));
                context.SaveChanges();
                return AsModel((TEntity)entity);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            using (var context = CreateContext())
            {
                var entity = context.DbSetAdd(AsEntity(model));
                await context.SaveChangesAsync();
                return AsModel((TEntity)entity);
            }
        }

        /// <inheritdoc/>
        public IDbSetSelector<TSet> FromDbSet<TSet>()
        {
            return new DbSetSelector<TSet>(this);
        }

        /// <inheritdoc/>
        public virtual void Update(TModel model)
        {
            using (var context = CreateContext())
            {
                var newEntityState = AsEntity(model);
                var entity = context.Find<TEntity>(GetPrimaryKeyValues(newEntityState));
                context.Entry(entity).CurrentValues.SetValues(newEntityState);
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TModel model)
        {
            using (var context = CreateContext())
            {
                var newEntityState = AsEntity(model);
                var entity = await context.FindAsync<TEntity>(GetPrimaryKeyValues(newEntityState));
                context.Entry(entity).CurrentValues.SetValues(newEntityState);
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public virtual void Delete(TModel model)
        {
            var entityToDelete = AsEntity(model);
            Delete(GetPrimaryKeyValues(entityToDelete));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TModel model)
        {
            var entityToDelete = AsEntity(model);
            await DeleteAsync(GetPrimaryKeyValues(entityToDelete));
        }

        /// <inheritdoc/>
        public void Delete(object[] key)
        {
            using (var context = CreateContext())
            {
                var entity = context.Find<TEntity>(key);
                context.Attach(entity);
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(object[] key)
        {
            using (var context = CreateContext())
            {
                var entity = await context.FindAsync<TEntity>(key);
                context.Attach(entity);
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public void Delete(object key)
        {
            Delete(new[] { key });
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(object key)
        {
            await DeleteAsync(new[] { key });
        }

        #endregion

        #region methods: private 

        private object[] GetPrimaryKeyValues(object entity)
        {
            return typeof(TEntity)
                .GetProperties()
                .Where(x => x.CustomAttributes.Count() > 0
                    && x.GetCustomAttributes<KeyAttribute>().Count() > 0)
                .Select(x => new
                {
                    Property = x,
                    KeyOrder = x.GetCustomAttributes<ColumnAttribute>().Count() > 0
                                    ? x.GetCustomAttributes<ColumnAttribute>().ToList()[0].Order
                                    : -1
                })
                .OrderBy(x => x.KeyOrder)
                .Select(x => x.Property.GetValue(entity)).ToArray();
        }

        #endregion
    }
}

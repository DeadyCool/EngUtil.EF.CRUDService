// --------------------------------------------------------------------------------
// <copyright filename="Repository.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// A abstract class with CRUD-Operations for Microsoft.EntityFrameworkCore <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext">Represents the o type of <see cref="DbContext"/></typeparam>
    /// <typeparam name="TEntity">Represents a <see cref="DbSet{TEntity}"/></typeparam>
    /// <typeparam name="TModel">Represents a Dto-Model for a <see cref="DbSet{TEntity}"/>y</typeparam>
    public abstract class Repository<TEntity, TModel> : ReadOnlyRepository<TEntity, TModel>, IRepository<TModel>, IRepositoryWriteDto<TEntity, TModel>
            where TEntity : class
            where TModel : class
    {

        #region ctor    

        /// <summary>
        /// Creates a instance with given parameter
        /// </summary>
        /// <param name="contextOptions">Represents the <see cref="DbContextOptions"/> with a specific <see cref="DbContext"/> to get access to the DbSets</param>
        protected Repository(DbContextOptions contextOptions)
            : base(contextOptions)
        {
        }

        #endregion

        #region properties

        /// <inheritdoc/>
        public virtual Expression<Func<TModel, TEntity>> AsEntityExpression { get; set; }   

        #endregion

        #region methods

        /// <inheritdoc/>
        public virtual TEntity AsEntity(TModel model)
        {
            return AsEntityExpression.Compile().Invoke(model);
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
        public virtual async Task<TModel> InsertAsync(TModel model, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var entity = context.DbSetAdd(AsEntity(model));
                await context.SaveChangesAsync(cancellationToken);
                return AsModel((TEntity)entity);
            }
        }

        /// <inheritdoc/>
        public virtual void InsertRange(IEnumerable<TModel> model)
        {
            using (var context = CreateContext())
            {
                var rangeToAdd = model.Select(x => AsEntity(x));
                context.AddRange(rangeToAdd);
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public virtual async Task InsertRangeAsync(IEnumerable<TModel> model, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var rangeToAdd = model.Select(x => AsEntity(x));
                await context.AddRangeAsync(rangeToAdd, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
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
        public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var newEntityState = AsEntity(model);
                var entity = await context.FindAsync<TEntity>(GetPrimaryKeyValues(newEntityState), cancellationToken);
                context.Entry(entity).CurrentValues.SetValues(newEntityState);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public virtual void Delete(TModel model)
        {
            var entityToDelete = AsEntity(model);
            Delete(GetPrimaryKeyValues(entityToDelete));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var entityToDelete = AsEntity(model);
            await DeleteAsync(GetPrimaryKeyValues(entityToDelete), cancellationToken);
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
        public async Task DeleteAsync(object[] key, CancellationToken cancellationToken = default)
        {
            using (var context = CreateContext())
            {
                var entity = await context.FindAsync<TEntity>(key);
                context.Attach(entity);
                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public void Delete(object key)
        {
            Delete(new[] { key });
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(object key, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(new[] { key }, cancellationToken);
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

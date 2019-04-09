﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using engUtil.EF.CRUDService.Core.Helper;

namespace engUtil.EF.CRUDService.Core.Base
{
    public abstract class RepositoryBase<TEntity, TModel> : IRepository<TModel>, IRepositoryDto<TEntity, TModel>
    {
        #region ctor

        public RepositoryBase(IDbContextService contextService)
        {
            DbContextService = contextService;
        }

        #endregion

        #region properties : protected

        protected IDbContextService DbContextService { get; set; }

        #endregion

        #region properties

        public virtual Expression<Func<TModel, TEntity>> AsEntityExpression { get; set; }

        public virtual Expression<Func<TEntity, TModel>> AsModelExpression { get; set; }

        #endregion

        #region mthods

        public virtual TEntity AsEntity(TModel model)
        {
            return AsEntityExpression.Compile().Invoke(model);
        }

        public virtual TModel AsModel(TEntity entity)
        {
            return AsModelExpression.Compile().Invoke(entity);
        }
               
        public virtual IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0)
        {
            return GetAsync(filter, orderBy, skip, take).Result;
        }

        public virtual async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0)
        {
            using (var ctx = DbContextService.CreateContext())
            {
                IQueryable<TModel> query;
                var queryableSet = ctx.GetDbSetAsIQuariable<TEntity>();
                if (queryableSet == null)
                    throw new NullReferenceException($"Could not found DbSet of Entity-Type { typeof(TEntity).Name } in DbContext!");
                if (filter != null)
                    query = queryableSet.Select(AsModelExpression).Where(filter);
                else
                    query = queryableSet.Select(AsModelExpression);
                if (orderBy != null)
                {
                    query = orderBy(query);
                    if (skip > 0)
                        query = query.Skip(skip);
                    if (take > 0)
                        query = query.Take(take);
                }
                return await query.ToListAsync();
            }          
        }

        public virtual TModel GetFirst(Expression<Func<TModel, bool>> filter)
        {
            return GetFirstAsync(filter).Result;        
        }

        public virtual async Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> filter)
        {
            using (var ctx = DbContextService.CreateContext())
            {
                IQueryable<TModel> query;
                var queryableSet = ctx.GetDbSetAsIQuariable<TEntity>();
                if (queryableSet == null)
                    throw new NullReferenceException($"Could not found DbSet of Entity-Type { typeof(TEntity).Name } in DbContext!");
                query = queryableSet.Select(AsModelExpression).Where(filter);
                return await query.FirstOrDefaultAsync();
            }
        }


        public virtual TModel Insert(TModel model)
        {
            return InsertAsync(model).Result;
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            using (var ctx = DbContextService.CreateContext())
            {              
                object entity = ctx.DbSetAdd(AsEntity(model));
                await ctx.SaveChangesAsync();
                return AsModel((TEntity)entity);
            }        
        }

        public virtual void Update(TModel model)
        {
            UpdateAsync(model).Wait();
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            using (var ctx = DbContextService.CreateContext())
            {
                var newEntityState = AsEntity(model);
                var entity = await ctx.FindAsync(typeof(TEntity), GetPrimaryKeyValues(newEntityState));
                ctx.Entry(entity).CurrentValues.SetValues(newEntityState);
                await ctx.SaveChangesAsync();
            }
        }

        public virtual void Delete(TModel model)
        {
            using (var ctx = DbContextService.CreateContext())
            {
                var entityToDelete = AsEntity(model);
                var entity = ctx.Find(typeof(TEntity), GetPrimaryKeyValues(entityToDelete));
                ctx.Attach(entity);
                ctx.Remove(entity);
                ctx.SaveChanges();
            }
        }        

        #endregion

        #region mthods: private     

        private Expression<Func<TModel, bool>> GetKeyExpression(TModel model)
        {
            bool singleExpression = true;
            var param = Expression.Parameter(typeof(TModel), "x");
            Expression expression = null;
            var properties = typeof(TModel).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.GetCustomAttribute<KeyAttribute>() != null)
                {
                    Expression propertyName = Expression.Property(param, prop.Name);
                    Expression propertyExpression = Expression.Constant(prop.GetValue(model), prop.PropertyType);
                    if (singleExpression)
                        expression = Expression.Equal(propertyName, propertyExpression);
                    else
                        expression = Expression.AndAlso(expression, Expression.Equal(propertyName, propertyExpression));
                    singleExpression = false;
                }
            }
            return Expression.Lambda<Func<TModel, bool>>(expression, param);
        }

        private string GetIdFieldName()
        {
            string[] fieldNames = typeof(TModel)
                .GetProperties()
                .Where(x => x.Name.ToLower().Contains("id"))
                .Select(n => n.Name)
                .ToArray();
            string fieldName = fieldNames.FirstOrDefault(x => x.ToLower() == "id");
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                if (fieldNames.Count(x => x.ToLower().Contains("id")) > 1)
                    throw new InvalidOperationException("Could not identify Entity! Ambiguous IDs!" +
                        "\r\nUse the function 'Delete(string idFieldName, int id)' with explicit ID-Fieldname");
                fieldName = fieldNames.FirstOrDefault(x => x.ToLower().Contains("id"));
            }
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new InvalidOperationException("Could not identify Entity! No Id-Field found!" +
                    "\r\nUse the function 'Delete(string idFieldName, int id)' with explicit ID-Fieldname");
            return fieldName;
        }

        private Expression<Func<TModel, bool>> GetIdExpression(string idFieldName, int id)
        {
            var param = Expression.Parameter(typeof(TModel), "x");
            Expression propertyName = Expression.Property(param, idFieldName);
            var idValue = Expression.Constant(id, typeof(int));
            Expression expression = Expression.Equal(propertyName, idValue);
            return Expression.Lambda<Func<TModel, bool>>(expression, param);
        }

        private object[] GetPrimaryKeyValues(object entity)
        {
            return typeof(TEntity)
                .GetProperties()
                .Where(x => x.CustomAttributes.Count() > 0 && x.GetCustomAttributes<KeyAttribute>().Count() > 0)
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

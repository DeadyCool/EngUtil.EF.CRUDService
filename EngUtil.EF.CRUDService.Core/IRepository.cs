// --------------------------------------------------------------------------------
// <copyright filename="IRepository.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// Represents a interface for common CRUD-Operations
    /// </summary>
    /// <typeparam name="TModel">Represents a type for a specific entity</typeparam>
    public interface IRepository<TModel>
        where TModel : class
    {
        /// <summary>
        /// Counts the number of entities inside the repository
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        /// <returns></returns>
        int Count(Expression<Func<TModel, bool>> filter = null);

        /// <summary>
        /// Counts the number of entities inside the repository
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TModel, bool>> filter = null);

        /// <summary>
        /// Returns the first entity from the repository
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        TModel GetFirst(Expression<Func<TModel, bool>> filter);

        /// <summary>
        /// Returns the first entity from the repository
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> filter);


        /// <summary>
        /// Returns a list of entities from the repository
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null,
                                           Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
                                           int skip = 0,
                                           int take = 0);

        /// <summary>
        /// Returns a list of entities from the repository
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> filter = null,
                                           Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
                                           int skip = 0,
                                           int take = 0);

        /// <summary>
        /// Inserts a entity in the repository
        /// </summary>
        /// <param name="model"></param>
        TModel Insert(TModel model);

        /// <summary>
        /// Inserts a entity in the repository
        /// </summary>
        /// <param name="model"></param>
        Task<TModel> InsertAsync(TModel model);

        /// <summary>
        /// Updates a entity in the repository
        /// </summary>
        /// <param name="model"></param>   
        void Update(TModel model);

        /// <summary>
        /// Updates a entity in the repository
        /// </summary>
        /// <param name="model"></param>
        Task UpdateAsync(TModel model);

        /// <summary>
        /// Remove a entity from the repository
        /// </summary>
        /// <param name="model"></param>
        void Delete(TModel model);

        /// <summary>
        /// Remove a entity from the repository
        /// </summary>
        /// <param name="model"></param>
        Task DeleteAsync(TModel model);

        /// <summary>
        /// Remove a entity from the repository
        /// </summary>
        /// <param name="key"></param>
        void Delete(object[] key);

        /// <summary>
        /// Remove a entity from the repository
        /// </summary>
        /// <param name="key"></param>
        Task DeleteAsync(object[] key);

        /// <summary>
        /// Remove a entity from the repository
        /// </summary>
        /// <param name="key"></param>
        void Delete(object key);

        /// <summary>
        /// Remove a entity from the repository
        /// </summary>
        /// <param name="key"></param>
        Task DeleteAsync(object key);

        /// <summary>
        /// Accesses a specific entity from the DbContext
        /// </summary>
        /// <typeparam name="TSet"></typeparam>
        /// <returns></returns>
        IDbSetAccessor<TSet> FromDbSet<TSet>();
    }
}

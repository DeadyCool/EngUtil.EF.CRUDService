// --------------------------------------------------------------------------------
// <copyright filename="IRepository.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    public interface IRepository<TModel>
        where TModel : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Count(Expression<Func<TModel, bool>> filter = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TModel, bool>> filter = null);

        /// <summary>
        /// Get first entity from the Repository
        /// </summary>
        /// <param name="filter">Expression Filter</param>
        TModel GetFirst(Expression<Func<TModel, bool>> filter);

        /// <summaryentity
        /// Get first TModel from the Repository
        /// </summary>
        /// <param name="filter">Expression Filter</param>
        Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> filter);


        /// <summary>
        /// Get List of entities from the Repository
        /// </summary>
        /// <param name="filter">Expression Filter</param>
        /// <param name="orderBy">Expression OrderBy</param>
        /// <param name="skip">Skip results, provided that OrderBy-Expression is set </param>
        /// <param name="take">Take specific count of results, provided that OrderBy-Expression is set</param>
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null,
                                           Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
                                           int skip = 0,
                                           int take = 0);
        /// <summary>
        /// Get List of entities from the Repository
        /// </summary>
        /// <param name="filter">Expression Filter</param>
        /// <param name="orderBy">Expression OrderBy</param>
        /// <param name="skip">Skip results, provided that OrderBy-Expression is set </param>
        /// <param name="take">Take specific count of results, provided that OrderBy-Expression is set</param>
        Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> filter = null,
                                           Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
                                           int skip = 0,
                                           int take = 0);

        /// <summary>
        /// Insert a model in the repository
        /// </summary>
        /// <param name="model"></param>
        TModel Insert(TModel model);

        /// <summary>
        /// Insert a model in the repository
        /// </summary>
        /// <param name="model"></param>
        Task<TModel> InsertAsync(TModel model);

        /// <summary>
        /// update the model in the repository
        /// </summary>
        /// <param name="model"></param>   
        void Update(TModel model);

        /// <summary>
        /// update the model in the repository
        /// </summary>
        /// <param name="model"></param>
        Task UpdateAsync(TModel model);

        /// <summary>
        /// remove the model from the repository
        /// </summary>
        /// <param name="model"></param>
        void Delete(TModel model);


    }
}

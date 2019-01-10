using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace engUtil.EF.CRUDService.Core.Base
{
    public interface IRepository<TModel>
    {
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
        /// delete the model in the repository by id
        /// </summary>
        /// <param name="id">id value (Default fieldname ID, _ID, ID_)</param>
        void Delete(int id);

        /// <summary>
        /// delete the model in the repository by id
        /// </summary>
        /// <param name="idFieldName">ID-FieldName</param>
        /// <param name="id">id value</param>
        void Delete(string idFieldName, int id);

        void Delete(TModel model);
    }
}

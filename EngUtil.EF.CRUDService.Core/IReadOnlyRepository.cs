// --------------------------------------------------------------------------------
// <copyright filename="IReadOnlyRepositoy.cs" date="14-08-2023">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    public interface IReadOnlyRepository<TModel> 
        where TModel : class
    {
        /// <summary>
        /// Counts the number of entries inside the repository.
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression.</param>
        /// <returns></returns>
        int Count(Expression<Func<TModel, bool>> filter = null);

        /// <summary>
        /// Counts the number of entries inside the repository.
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TModel, bool>> filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the first entry from the repository.
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression.</param>
        TModel GetFirst(Expression<Func<TModel, bool>> filter);

        /// <summary>
        /// Returns the first entry from the repository.
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// Returns a list of entities from the repository.
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression.</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order.</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Returns a list of entities from the repository.
        /// </summary>
        /// <param name="filter">Specifies a filter as lambda expression.</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order.</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0, CancellationToken cancellationToken = default);

        /// <summary>
        /// Allows to filter and distinct select an <see cref="Microsoft.entryFrameworkCore.DbSet{Tentry}Set"/> from a <see cref="Microsoft.entryFrameworkCore.DbContext"/> entry
        /// </summary>
        /// <param name="filter">Specifies a filter expression.</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order.</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <returns></returns>
        IEnumerable<TModel> Distinct(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Allows to filter and distinct select an <see cref="Microsoft.entryFrameworkCore.DbSet{Tentry}Set"/> from a <see cref="Microsoft.entryFrameworkCore.DbContext"/> entry
        /// </summary>
        /// <param name="filter">Specifies a filter expression.</param>
        /// <param name="orderBy">Specifies a delegation function that arranges the entities in a specific order.</param>
        /// <param name="skip">Skip specific count of results, provided that the OrderBy expression parameter was specified. </param>
        /// <param name="take">Take specific count of results, provided that the OrderBy expression parameter was specified.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> DistinctAsync(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, int skip = 0, int take = 0, CancellationToken cancellationToken = default);

    }
}

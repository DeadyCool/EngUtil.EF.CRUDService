// --------------------------------------------------------------------------------
// <copyright filename="IRepository.cs" date="18-08-2023">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// Represents a interface for common CRUD-Operations
    /// </summary>
    /// <typeparam name="TModel">Represents a type for a specific entry</typeparam>
    public interface IRepository<TModel> : IReadOnlyRepository<TModel>
        where TModel : class
    {
        /// <summary>
        /// Inserts a new entry in the repository.
        /// </summary>
        /// <param name="model">Specifies the entry to be inserted.</param>
        TModel Insert(TModel model);

        /// <summary>
        /// Inserts a new entry in the repository.
        /// </summary>
        /// <param name="model">Specifies the entry to be inserted.</param>
        Task<TModel> InsertAsync(TModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts a range of entries in the repository.
        /// </summary>
        /// <param name="model">Specifies the collection of entries to be inserted.</param>
        void InsertRange(IEnumerable<TModel> model);

        /// <summary>
        /// Inserts a range of entries in the repository.
        /// </summary>
        /// <param name="model">Specifies the collection of entries to be inserted.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        Task InsertRangeAsync(IEnumerable<TModel> model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a entry in the repository.
        /// </summary>
        /// <param name="model">Specifies the entry to be updated.</param>   
        void Update(TModel model);

        /// <summary>
        /// Updates a entry in the repository.
        /// </summary>
        /// <param name="model">Specifies the entry to be updated.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task UpdateAsync(TModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a entry from the repository.
        /// </summary>
        /// <param name="model">Specifies the entry to be removed.</param>
        void Delete(TModel model);

        /// <summary>
        /// Removes a entry from the repository.
        /// </summary>
        /// <param name="model">Specifies the entry to be removed.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task DeleteAsync(TModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a entry from the repository.
        /// </summary>
        /// <param name="key">Specifies the corresponding keys to be removed.</param>
        void Delete(object[] key);

        /// <summary>
        /// Removes a entry from the repository.
        /// </summary>
        /// <param name="key">Specifies the corresponding keys to be removed.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task DeleteAsync(object[] key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a entry from the repository.
        /// </summary>
        /// <param name="key">Specifies the corresponding key to be removed.</param>
        void Delete(object key);

        /// <summary>
        /// Removes a entry from the repository.
        /// </summary>
        /// <param name="key">Specifies the corresponding key to be removed.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task DeleteAsync(object key, CancellationToken cancellationToken = default);
    }
}

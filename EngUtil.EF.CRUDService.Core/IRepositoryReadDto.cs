// --------------------------------------------------------------------------------
// <copyright filename="IRepositoryDto.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core
{
    public interface IRepositoryReadDto<TEntity, TModel>
        where TModel : class
        where TEntity : class
    {
        /// <summary>
        /// Transformation expression from Entity to Model.
        /// </summary>
        Expression<Func<TEntity, TModel>> AsModelExpression { get; set; }

        /// <summary>
        /// Transform Entity to Model.
        /// </summary>
        TModel AsModel(TEntity entity);
    }
}

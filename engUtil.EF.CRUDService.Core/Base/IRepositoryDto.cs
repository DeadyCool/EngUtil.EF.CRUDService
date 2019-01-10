using System;
using System.Linq.Expressions;

namespace engUtil.EF.CRUDService.Core.Base
{
    public interface IRepositoryDto<TEntity, TModel>
    {
        /// <summary>
        /// Transformation expression from Model to Entity
        /// </summary>
        Expression<Func<TModel, TEntity>> AsEntityExpression { get; set; }

        /// <summary>
        /// Transformation expression from Entity to Model
        /// </summary>
        Expression<Func<TEntity, TModel>> AsModelExpression { get; set; }

        /// <summary>
        /// Transform Model to Entity
        /// </summary>
        TEntity AsEntity(TModel model);

        /// <summary>
        /// Transform Entity to Model
        /// </summary>
        TModel AsModel(TEntity entity);
    }
}

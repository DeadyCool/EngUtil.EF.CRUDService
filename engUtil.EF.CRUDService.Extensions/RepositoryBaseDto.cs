// --------------------------------------------------------------------------------
// <copyright filename="RepositoryBaseDto.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;
using engUtil.Dto;
using engUtil.EF.CRUDService.Base;

namespace engUtil.EF.CRUDService.Extensions
{
    public abstract class RepositoryBaseDto<TEntity, TModel> : RepositoryBase<TEntity, TModel>
    {
        public RepositoryBaseDto(IDbContextService contextService) 
            : base(contextService)
        {
        }

        public RepositoryBaseDto(IDbContextService contextService, IMapper dtoMapper) 
            : base(contextService)
        {           
            AsEntityExpression = (Expression<Func<TModel, TEntity>>)dtoMapper.GetExpressionMap(typeof(TModel), typeof(TEntity));
            AsModelExpression = (Expression<Func<TEntity, TModel>>)dtoMapper.GetExpressionMap(typeof(TEntity), typeof(TModel));
        }
    }
}

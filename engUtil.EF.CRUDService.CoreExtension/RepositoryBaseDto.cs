using System;
using System.Linq.Expressions;
using engUtil.Dto;
using engUtil.EF.CRUDService.Core.Base;

namespace engUtil.EF.CRUDService.Extensions
{
    [MapDefinition]
    public abstract class RepositoryBaseDto<TEntity, TModel> : RepositoryBase<TEntity, TModel>
    {
        protected IMapper Mapper;

        public RepositoryBaseDto(IDbContextService contextService) 
            : base(contextService)
        {
        }

        public RepositoryBaseDto(IDbContextService contextService, IMapper dtoMapper) 
            : base(contextService)
        {
            Mapper = dtoMapper;
            AsEntityExpression = (Expression<Func<TModel, TEntity>>)dtoMapper.GetExpressionMap(typeof(TModel), typeof(TEntity));
            AsModelExpression = (Expression<Func<TEntity, TModel>>)dtoMapper.GetExpressionMap(typeof(TEntity), typeof(TModel));
            if (AsEntityExpression == null 
                || AsModelExpression == null)
                dtoMapper.GetMapDefinition(this);
        }
    }
}

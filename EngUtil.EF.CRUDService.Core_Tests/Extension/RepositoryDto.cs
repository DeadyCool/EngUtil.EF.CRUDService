using engUtil.Dto;
using EngUtil.EF.CRUDService.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Extension
{
    public abstract class RepositoryDto<TDbContext, TEntity, TModel> : Repository<TDbContext, TEntity, TModel>
        where TDbContext : DbContext
        where TEntity : class
        where TModel : class
    {
        protected RepositoryDto(ISessionContext<TDbContext> contextService)
            : base(contextService)
        {
        }

        protected RepositoryDto(ISessionContext<TDbContext> contextService, IMapper dtoMapper)
            : base(contextService)
        {
            AsEntityExpression = (Expression<Func<TModel, TEntity>>)dtoMapper.GetExpressionMap(typeof(TModel), typeof(TEntity));
            AsModelExpression = (Expression<Func<TEntity, TModel>>)dtoMapper.GetExpressionMap(typeof(TEntity), typeof(TModel));
            if (AsEntityExpression == null
                || AsModelExpression == null)
                dtoMapper.GetMapDefinition(this);
        }

        protected RepositoryDto(DbContextOptions<TDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        protected RepositoryDto(DbContextOptions<TDbContext> contextOptions, IMapper dtoMapper)
            : base(contextOptions)
        {
            AsEntityExpression = (Expression<Func<TModel, TEntity>>)dtoMapper.GetExpressionMap(typeof(TModel), typeof(TEntity));
            AsModelExpression = (Expression<Func<TEntity, TModel>>)dtoMapper.GetExpressionMap(typeof(TEntity), typeof(TModel));
            if (AsEntityExpression == null
                || AsModelExpression == null)
                dtoMapper.GetMapDefinition(this);
        }
    }
}

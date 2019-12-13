// --------------------------------------------------------------------------------
// <copyright filename="RepositoryBaseDto.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;
using engUtil.Dto;
using EngUtil.EF.CRUDService.Core.Base;
using EngUtil.EF.CRUDService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Extensions
{
    public abstract class RepositoryBaseDto<TDbContext, TEntity, TModel> : RepositoryBase<TDbContext, TEntity, TModel>
        where TDbContext : DbContext
    {
        protected IMapper Mapper;

        public RepositoryBaseDto(ISessionContext<TDbContext> contextService) 
            : base(contextService)
        {
        }

        public RepositoryBaseDto(ISessionContext<TDbContext> contextService, IMapper dtoMapper) 
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

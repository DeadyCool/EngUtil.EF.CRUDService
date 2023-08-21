using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class NewsRepository : Repository<NewsEntity, NewsModel>
    {
        public NewsRepository(DbContextOptions<NewspaperContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<NewsModel, NewsEntity>> AsEntityExpression => Dto.ToNewsEntity;

        public override Expression<Func<NewsEntity, NewsModel>> AsModelExpression => Dto.ToNewsModel;
    }
}

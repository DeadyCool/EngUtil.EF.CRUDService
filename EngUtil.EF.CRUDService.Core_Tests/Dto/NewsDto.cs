using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static partial class Dto
    {
        public static Expression<Func<NewsEntity, NewsModel>> ToNewsModel =>
            x => new NewsModel
            {
                Id = x.RecId,      
                AuthoredOn = x.Created.ToString("U"),
                Content = x.Content,
                Header = x.Header,  
                ReporterName = x.Reporter == null ? "" : $"{x.Reporter.Surename} {x.Reporter.Name}",
                ReporterId = x.ReporterId, 
                Created = x.Created,
                Updated = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            };

        public static Expression<Func<NewsModel, NewsEntity>> ToNewsEntity =>
            x => new NewsEntity
            {
                RecId = x.Id,           
                Content = x.Content,
                Header = x.Header,        
                ReporterId = x.ReporterId,
                Created = x.Created,
                Updated = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            };
    }
}

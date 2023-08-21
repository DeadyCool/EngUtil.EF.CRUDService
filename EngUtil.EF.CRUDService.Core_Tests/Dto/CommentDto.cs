using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static partial class Dto  
    {     
        public static Expression<Func<CommentEntity, CommentModel>> ToCommentModel =>
            x => new CommentModel
            {
                Id = x.RecId,
                NewsId = x.NewsId,  
                UserId = x.UserId,                
                AuthoredOn = x.Created.ToString("U"),
                Content = x.Content,
                CommentatorName = x.User == null ? "" : $"{x.User.Surename} {x.User.Name}",          
                Created = x.Created,
                Updated = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            };
             
        public static Expression<Func<CommentModel, CommentEntity>> ToCommentEntity =>
            x => new CommentEntity
            {
                RecId = x.Id,
                UserId = x.UserId,
                NewsId = x.NewsId,
                Content = x.Content,           
                Created = x.Created,
                Updated = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            };
    }
}

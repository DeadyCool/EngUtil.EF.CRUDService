using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class CommentRepository : Repository<CommentEntity, CommentModel>
    {
        public CommentRepository(DbContextOptions<NewspaperContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<CommentModel, CommentEntity>> AsEntityExpression => Dto.ToCommentEntity;

        public override Expression<Func<CommentEntity, CommentModel>> AsModelExpression => Dto.ToCommentModel;
    }
}

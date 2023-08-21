using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class UserRepository : Repository<UserEntity, UserModel>
    {
        public UserRepository(DbContextOptions<NewspaperContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<UserModel, UserEntity>> AsEntityExpression => Dto.ToUserEntity;

        public override Expression<Func<UserEntity, UserModel>> AsModelExpression => Dto.ToUserModel;
    }
}

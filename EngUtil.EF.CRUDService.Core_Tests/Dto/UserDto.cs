using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static partial class Dto
    {
        public static Expression<Func<UserEntity, UserModel>> ToUserModel =>
            x => new UserModel
            {
                Id = x.RecId,
                State = x.State,
                StreetAddress = x.StreetAddress,
                DayOfBirth = x.DayOfBirth,  
                Surename = x.Surename,  
                EMail = x.EMail,
                Location = x.Location,
                Name = x.Name,
                Password = x.Password,
                ZIPCode = x.ZIPCode,
                Created = x.Created,
                Updated = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            };
        
        public static Expression<Func<UserModel, UserEntity>> ToUserEntity =>
            x => new UserEntity
            {
                RecId = x.Id,
                State = x.State,
                StreetAddress = x.StreetAddress,
                DayOfBirth = x.DayOfBirth,
                Surename = x.Surename,
                EMail = x.EMail,
                Location = x.Location,
                Name = x.Name,
                Password = x.Password,
                ZIPCode = x.ZIPCode,
                Created = x.Created,
                Updated = x.Updated,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            };
    }
}

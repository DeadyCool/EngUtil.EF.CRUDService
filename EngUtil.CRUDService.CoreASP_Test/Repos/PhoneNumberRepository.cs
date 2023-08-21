using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.EF.CRUDService.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EngUtil.CRUDService.CoreASP_Test.Repos
{
    public class PhoneNumberRepository : Repository<PhoneNumberEntity, PhoneNumberModel>
    {
        public PhoneNumberRepository(DbContextOptions<PhoneBookContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<PhoneNumberModel, PhoneNumberEntity>> AsEntityExpression => Dto.ToPhoneNumberEntity;

        public override Expression<Func<PhoneNumberEntity, PhoneNumberModel>> AsModelExpression => Dto.ToPhoneNumberModel;
    }
}

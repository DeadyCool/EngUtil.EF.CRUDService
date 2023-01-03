using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class PhoneNumberRepository : Repository<AddressBookContext, PhoneNumberEntity, TelefonnummerModel>
    {
        public PhoneNumberRepository(DbContextOptions<AddressBookContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<TelefonnummerModel, PhoneNumberEntity>> AsEntityExpression => Dto.ToPhoneNumberEntity;

        public override Expression<Func<PhoneNumberEntity, TelefonnummerModel>> AsModelExpression => Dto.ToPhoneNumberModel;
    }
}

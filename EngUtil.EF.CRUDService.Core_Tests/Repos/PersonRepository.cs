using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class PersonRepository : Repository<AddressBookContext, PersonEntity, PersonModel>
    {
        public PersonRepository(DbContextOptions<AddressBookContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<PersonModel, PersonEntity>> AsEntityExpression => Dto.ToPersonEntity;

        public override Expression<Func<PersonEntity, PersonModel>> AsModelExpression => Dto.ToPersonModel;
    }
}

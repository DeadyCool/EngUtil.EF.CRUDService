using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.EF.CRUDService.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EngUtil.CRUDService.CoreASP_Test.Repos
{
    public class PersonRepository : Repository<PersonEntity, PersonModel>
    {
        public PersonRepository(DbContextOptions<PhoneBookContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<PersonModel, PersonEntity>> AsEntityExpression=> Dto.ToPersonEntity;

        public override Expression<Func<PersonEntity, PersonModel>> AsModelExpression => Dto.ToPersonModel;

    }
}

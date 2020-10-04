using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.EF.CRUDService.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EngUtil.CRUDService.CoreASP_Test.Repos
{
    public class PersonRepository : Repository<PhoneBookContext, PersonEntity, PersonModel>
    {
        public PersonRepository(DbContextOptions<PhoneBookContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<PersonModel, PersonEntity>> AsEntityExpression
        {
            get => x => new PersonEntity
            {
                RecId = x.Id,
                Name = x.Forename,
                Surename = x.Surename,
                FullName = $"{x.Surename} {x.Forename}"

            };
            set => base.AsEntityExpression = value;
        }

        public override Expression<Func<PersonEntity, PersonModel>> AsModelExpression
        {
            get => x => new PersonModel
            {
                Id = x.RecId,
                Forename = x.Name,
                Surename = x.Surename
            };
            set => base.AsModelExpression = value;
        }
    }
}

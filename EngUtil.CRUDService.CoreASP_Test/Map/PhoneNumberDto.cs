using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using System;
using System.Linq.Expressions;

namespace EngUtil.CRUDService.CoreASP_Test
{
    public static partial class Dto
    {
        public static Expression<Func<PhoneNumberEntity, PhoneNumberModel>> ToPhoneNumberModel =>
            x => new PhoneNumberModel
            {             
                Id = x.RecId,
                NumberType = x.NumberType.ToString(),
                Number = x.Number,
                Person = x.Person != null ? new PersonModel
                {
                    Id = x.Person.RecId,
                    Forename = x.Person.Name,
                    Surename = x.Person.Surename
                } : default
            };
      
        public static Expression<Func<PhoneNumberModel, PhoneNumberEntity>> ToPhoneNumberEntity =>
            x => new PhoneNumberEntity
            {
                RecId = x.Id,
                Number = x.Number,
                NumberType = Enum.Parse<NumberType>(x.NumberType),               
                Person = x.Person != null ? new PersonEntity
                {
                    RecId = x.Person.Id,
                    Name = x.Person.Forename,
                    Surename = x.Person.Surename,
                    FullName = $"{x.Person.Surename} {x.Person.Forename}"
                } : default
            };
    }
}

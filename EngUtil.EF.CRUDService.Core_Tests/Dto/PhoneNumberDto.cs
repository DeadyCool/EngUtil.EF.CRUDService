using engUtil.Dto;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Dto
{
    public class PhoneNumberDto : MapDefinition
    {
        [Map] 
        public Expression<Func<PhoneNumberEntity, TelefonnummerModel>> ToModelDto =>
            x => new TelefonnummerModel
            {
                Id = x.RecId,
                Nummer = x.Number,
                Typ = x.NumberType.ToString(),
                PersonId = x.PersonId,
                Person = x.Person != null ? MapTo<PersonModel>(x.Person) : default
            };

        [Map]
        public Expression<Func<TelefonnummerModel, PhoneNumberEntity>> ToEntityDto =>
            x => new PhoneNumberEntity
            {
                RecId = x.Id,
                Number = x.Nummer,
                NumberType = Enum.Parse<NumberType>(x.Typ),
                PersonId = x.PersonId
            };
    }
}

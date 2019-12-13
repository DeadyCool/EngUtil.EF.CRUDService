using engUtil.Dto;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Dto
{
    public class PhoneNumberDto : MapDefinition
    {
        [Map] 
        public Expression<Func<PhoneNumberEntity, PhoneNumberModel>> ToModelDto =>
            x => new PhoneNumberModel
            {
                Id = x.RecId,
                Number = x.Number,
                NumberType = x.NumberType.ToString(),
                PersonId = x.PersonId,
                Person = x.Person != null ? MapTo<PersonModel>(x.Person) : default(PersonModel)
            };

        [Map]
        public Expression<Func<PhoneNumberModel, PhoneNumberEntity>> ToEntityDto =>
            x => new PhoneNumberEntity
            {
                RecId = x.Id,
                Number = x.Number,
                NumberType = Enum.Parse<NumberType>(x.NumberType),
                PersonId = x.PersonId
            };
    }
}

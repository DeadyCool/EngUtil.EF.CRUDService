using engUtil.Dto;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Dto
{
    public class PersonDto : MapDefinition
    {
        [Map]
        public Expression<Func<PersonEntity, PersonModel>> ToModelDto =>
            x => new PersonModel
            {
                Id = x.RecId,         
                Forename = x.Name,
                Surename = x.Surename,
                Name = $"{x.Surename} {x.Name}",
                Numbers = MapTo<PhoneNumberModel>(x.Numbers),
                EMails = MapTo<EmailModel>(x.EMails)
            };

        [Map]
        public Expression<Func<PersonModel, PersonEntity>> ToEntityDto =>
            x => new PersonEntity
            {
                RecId = x.Id,         
                Name = x.Forename,
                Surename = x.Surename                
            };
    }
}

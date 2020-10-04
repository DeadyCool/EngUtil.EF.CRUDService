using engUtil.Dto;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Dto
{
    public class EmailDto : MapDefinition
    {
        [Map]
        public Expression<Func<EmailEntity, EmailModel>> ToModelDto =>
            x => new EmailModel
            {
                Id = x.RecId,    
                PersonId = x.PersonId,
                Person = x.Person != null ? MapTo<PersonModel>(x.Person) : default,
                EMailAdresse = x.EMailAddress
            };

        [Map]
        public Expression<Func<EmailModel, EmailEntity>> ToEntityDto =>
            x => new EmailEntity
            {
                RecId = x.Id,
                PersonId = x.PersonId,
                EMailAddress = x.EMailAdresse
            };
    }
}

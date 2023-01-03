
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EngUtil.CRUDService.CoreASP_Test
{
    public static partial class Dto  
    {     
        public static Expression<Func<PersonEntity, PersonModel>> ToPersonModel =>
            x => new PersonModel
            {
                Id = x.RecId,
                Forename = x.Name,
                Surename = x.Surename,
                EMails = x.EMails != null ? x.EMails.Select( e=> new EmailModel
                {
                    Id = e.RecId,
                    EMailAddress = e.EMailAddress
                }) : default,
                PhoneNumbers = x.Numbers != null ? x.Numbers.Select(e => new PhoneNumberModel
                {
                    Id = e.RecId,                       
                    NumberType = e.NumberType.ToString(),
                    Number = e.Number,
                }) : default,
            };
             
        public static Expression<Func<PersonModel, PersonEntity>> ToPersonEntity =>
            x => new PersonEntity
            {
                RecId = x.Id,
                Name = x.Forename,
                Surename = x.Surename,
                FullName = $"{x.Surename} {x.Forename}",
                EMails = x.EMails != null ? x.EMails.Select(e => new EmailEntity
                {
                    RecId = e.Id,
                    PersonId = x.Id,
                    EMailAddress = e.EMailAddress
                }).ToList() : default,
                Numbers = x.PhoneNumbers != null ? x.PhoneNumbers.Select(e => new PhoneNumberEntity
                {
                    RecId = e.Id,
                    PersonId = x.Id,
                    NumberType = Enum.Parse<NumberType>(e.NumberType),
                    Number = e.Number,
                }).ToList() : default,
            };
    }
}

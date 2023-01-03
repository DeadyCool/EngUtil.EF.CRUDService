using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using System;
using System.Linq.Expressions;

namespace EngUtil.CRUDService.CoreASP_Test
{
    public static partial class Dto
    {
       
        public static Expression<Func<EmailEntity, EmailModel>> ToEmailModel =>
            x => new EmailModel
            {
                Id = x.RecId,    
                Person = x.Person != null ? new PersonModel
                {
                    Id = x.Person.RecId,
                    Forename = x.Person.Name,
                    Surename = x.Person.Surename
                } : default,
                EMailAddress = x.EMailAddress
            };
        
        public static Expression<Func<EmailModel, EmailEntity>> ToEmailEntity =>
            x => new EmailEntity
            {
                RecId = x.Id,
                Person = x.Person != null ? new PersonEntity
                {
                    RecId = x.Person.Id,             
                    Name = x.Person.Forename,
                    Surename = x.Person.Surename,
                    FullName = $"{x.Person.Surename} {x.Person.Forename}"
                } : default,
                EMailAddress = x.EMailAddress
            };
    }
}

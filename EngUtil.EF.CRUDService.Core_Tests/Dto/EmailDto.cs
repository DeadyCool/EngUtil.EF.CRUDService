using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static partial class Dto
    {
       
        public static Expression<Func<EmailEntity, EmailModel>> ToEmailModel =>
            x => new EmailModel
            {
                Id = x.RecId,    
                PersonId = x.PersonId,
                Person = x.Person != null ? new PersonModel
                {
                    Id = x.Person.RecId,
                    Vorname = x.Person.Name,
                    Nachname = x.Person.Surename,
                    Bundesland = x.Person.State,
                    Erstellt = x.Person.Created,
                    Ort = x.Person.Location,
                    PLZ = x.Person.ZIPCode,
                    Geburtstag = x.Person.DayOfBirth,
                    Strasse = x.Person.StreetAddress,
                    Name = $"{x.Person.Surename} {x.Person.Name}"
                } : default,
                EMailAdresse = x.EMailAddress
            };
        
        public static Expression<Func<EmailModel, EmailEntity>> ToEmailEntity =>
            x => new EmailEntity
            {
                RecId = x.Id,
                PersonId = x.PersonId,
                EMailAddress = x.EMailAdresse
            };
    }
}

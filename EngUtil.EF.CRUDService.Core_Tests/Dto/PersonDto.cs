using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static partial class Dto  
    {     
        public static Expression<Func<PersonEntity, PersonModel>> ToPersonModel =>
            x => new PersonModel
            {
                Id= x.RecId,
                Vorname = x.Name,
                Nachname = x.Surename,
                Bundesland = x.State,
                Erstellt = x.Created,
                Ort = x.Location,
                PLZ = x.ZIPCode,
                Geburtstag = x.DayOfBirth,
                Strasse = x.StreetAddress,
                Name = $"{x.Surename} {x.Name}",
                Telefonnummern = x.Numbers != null ? x.Numbers.Select(n => new TelefonnummerModel
                {
                    Id = n.RecId,
                    Nummer = n.Number,
                    Typ = n.NumberType.ToString(),
                    PersonId = n.PersonId                  
                }) : default,
                EMailadressen = x.EMails != null ? x.EMails.Select(n => new EmailModel
                {
                    Id = n.RecId,
                    PersonId = n.PersonId,              
                    EMailAdresse = n.EMailAddress
                }) : default
            };
             
        public static Expression<Func<PersonModel, PersonEntity>> ToPersonEntity =>
            x => new PersonEntity
            {
                RecId = x.Id,         
                Name = x.Vorname,
                Surename = x.Nachname,
                State = x.Bundesland,
                Created = x.Erstellt,
                DayOfBirth = x.Geburtstag,
                Location = x.Ort,
                ZIPCode = x.PLZ,
                StreetAddress = x.Strasse
            };
    }
}

using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static partial class Dto
    {
        
        public static Expression<Func<PhoneNumberEntity, TelefonnummerModel>> ToPhoneNumberModel =>
            x => new TelefonnummerModel
            {
                Id = x.RecId,
                Nummer = x.Number,
                Typ = x.NumberType.ToString(),
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
                } : default
            };

      
        public static Expression<Func<TelefonnummerModel, PhoneNumberEntity>> ToPhoneNumberEntity =>
            x => new PhoneNumberEntity
            {
                RecId = x.Id,
                Number = x.Nummer,
                NumberType = Enum.Parse<NumberType>(x.Typ),
                PersonId = x.PersonId
            };
    }
}

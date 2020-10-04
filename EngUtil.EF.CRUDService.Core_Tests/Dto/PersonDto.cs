using engUtil.Dto;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using System;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core_Tests.Dto
{
    public class PersonDto : MapDefinition
    {
        [Map]
        public Expression<Func<PersonEntity, PersonModel>> ToModelDto =>
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
                Telefonnummern = MapTo<TelefonnummerModel>(x.Numbers),
                EMailadressen = MapTo<EmailModel>(x.EMails)
            };

        [Map]
        public Expression<Func<PersonModel, PersonEntity>> ToEntityDto =>
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

using System;
using System.Collections.Generic;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class PersonModel : ModelBase
    {
        public IEnumerable<EmailModel> EMailadressen { get; set; }

        public IEnumerable<TelefonnummerModel> Telefonnummern { get; set; }

        public string Name { get; set; }

        public string Nachname { get; set; }

        public string Vorname { get; set; }

        public string Strasse { get; set; }

        public string Ort { get; set; }

        public DateTime Erstellt { get; set; }

        public string Bundesland { get; set; }

        public string PLZ { get; internal set; }

        public DateTime Geburtstag { get; internal set; }
    }
}

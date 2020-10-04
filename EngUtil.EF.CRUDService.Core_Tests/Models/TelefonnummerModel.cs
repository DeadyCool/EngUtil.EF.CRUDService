using System;
using System.Collections.Generic;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class TelefonnummerModel : ModelBase
    {
        public PersonModel Person { get; set; }
        public Guid PersonId { get; set; }
        public string Typ { get; set; }
        public string Nummer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class PersonModel : ModelBase
    {
        public IEnumerable<EmailModel> EMails { get; set; }
        public IEnumerable<PhoneNumberModel> Numbers { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Forename { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EngUtil.CRUDService.CoreASP_Test.Models
{
    public class PersonModel : ModelBase
    {
        public string Surename { get; set; }
        
        public string Forename { get; set; }

        public IEnumerable<EmailModel> EMails { get; set; }      

        public IEnumerable<PhoneNumberModel> PhoneNumbers { get; set; }
    }
}

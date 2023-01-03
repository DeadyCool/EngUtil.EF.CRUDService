using System;

namespace EngUtil.CRUDService.CoreASP_Test.Models
{
    public class PhoneNumberModel : ModelBase
    {
        public string NumberType { get; set; }

        public string Number { get; set; }

        public PersonModel Person { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class PhoneNumberModel : ModelBase
    {
        public PersonModel Person { get; set; }
        public Guid PersonId { get; set; }
        public string NumberType { get; set; }
        public string Number { get; set; }
    }
}

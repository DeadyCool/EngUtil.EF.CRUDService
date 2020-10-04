using System;

namespace EngUtil.CRUDService.CoreASP_Test.Models
{
    public class PhoneNumberModel : ModelBase
    {
        public Guid PersonId { get; set; }
        public string NumberType { get; set; }
        public string Number { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class EmailModel : ModelBase
    {
        public Guid PersonId { get; set; }
        public PersonModel Person { get; set; }
        public string EMailAddress { get; internal set; }
    }
}

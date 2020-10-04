using System;

namespace EngUtil.CRUDService.CoreASP_Test.Models
{
    public class EmailModel : ModelBase
    {
        public Guid PersonId { get; set; }
        public string EMailAddress { get; set; }
    }
}

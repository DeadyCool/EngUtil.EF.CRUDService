using System;
using System.Collections.Generic;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class NewsModel : ModelBase
    {
        public Guid ReporterId { get; set; }

        public string ReporterName { get; set; }

        public string AuthoredOn { get; set; }  

        public string Header { get; set; }

        public string Content { get; set; }
    }
}

using System;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class CommentModel : ModelBase
    {
        public Guid UserId { get; set; }

        public string CommentatorName { get; set; }

        public string AuthoredOn { get; set; }

        public string Content { get; set; }
        public Guid NewsId { get; internal set; }
    }
}

using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;

namespace EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities
{
    public class NewsEntity : EntityBase
    {
        public Guid ReporterId { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public UserEntity Reporter { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}

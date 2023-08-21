// --------------------------------------------------------------------------------
// <copyright filename="PersonEntity.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities;

namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities
{
    public class UserEntity : EntityBase
    {
        public string Name { get; set; }

        public string Surename { get; set; }

        public string? EMail { get; set; } 

        public string StreetAddress { get; set; }

        public string Location { get; set; }

        public string State { get; set; }

        public string ZIPCode { get; set; }

        public string Password { get; set; }

        public DateTime DayOfBirth { get; set; }

        public ICollection<NewsEntity> News { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}

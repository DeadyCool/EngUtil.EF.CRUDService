// --------------------------------------------------------------------------------
// <copyright filename="PhoneNumberEntity.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities;


namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities
{
    public class CommentEntity : EntityBase
    {
        public string Content { get; set; }   

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        public Guid NewsId { get; set; }

        public NewsEntity News { get; set; }
    }
}

// --------------------------------------------------------------------------------
// <copyright filename="EntityBase.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            RecId = Guid.Empty;
            Created = DateTime.Now;
        }

        [Key]
        public Guid RecId { get; set; }

        public DateTime Created {get;set;}

        public Guid CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}

using System;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public abstract class ModelBase 
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Created { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}

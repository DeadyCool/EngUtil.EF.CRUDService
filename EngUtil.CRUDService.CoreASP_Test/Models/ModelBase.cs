using System;

namespace EngUtil.CRUDService.CoreASP_Test.Models
{
    public abstract class ModelBase
    {
        private Guid _id;

        protected ModelBase()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id 
        {   get
            {
                if (_id == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                };
                return _id;
            }
            set => _id = value; 
        }
    }
}

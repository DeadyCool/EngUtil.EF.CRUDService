// --------------------------------------------------------------------------------
// <copyright filename="RepositoryContextSession.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core
{
    public abstract class RepositoryContextSession<TDbContext>
        where TDbContext : DbContext
    { 
        protected internal ISessionContext<TDbContext> SessionContext;

        protected internal string ConnectionString;

        public RepositoryContextSession(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public RepositoryContextSession (ISessionContext<TDbContext> sessionContext)
        {
            SessionContext = sessionContext;            
        }
    }
}

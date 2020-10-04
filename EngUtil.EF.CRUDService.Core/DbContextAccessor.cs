// --------------------------------------------------------------------------------
// <copyright filename="DbContextAccessor.cs" date="20-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace EngUtil.EF.CRUDService.Core
{
    public abstract class DbContextAccessor<TDbContext> : DbSetSelector, IDisposable, IDbContextAccessor
        where TDbContext : DbContext
    {
        #region protected

        protected internal ISessionContext<TDbContext> SessionContext;

        #endregion

        #region fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _disposed = false;

        #endregion

        #region ctor

        public DbContextAccessor(DbContextOptions<TDbContext> contextOptions)
        {
            Options = contextOptions;
        }

        public DbContextAccessor(ISessionContext<TDbContext> sessionContext)
        {
            SessionContext = sessionContext;
        }

        #endregion

        #region mothods

        public virtual DbContext CreateContext()
        {
            if (Options != null)
                return (TDbContext)Activator.CreateInstance(typeof(TDbContext), Options);
            else if (SessionContext != null)
                return SessionContext.GetContext();
            throw new Exception("Session or DbContextOptions missing");
        }

        #endregion

        #region cleanup

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (SessionContext != null)
                    DbContextInternal.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~DbContextAccessor() => Dispose(false);

        #endregion
    }
}

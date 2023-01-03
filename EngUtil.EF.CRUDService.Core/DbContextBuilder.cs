// --------------------------------------------------------------------------------
// <copyright filename="DbContextBuilder.cs" date="20-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace EngUtil.EF.CRUDService.Core
{
    public abstract class DbContextBuilder<TDbContext> : IDisposable, IDbContextBuilder<TDbContext>
        where TDbContext : DbContext
    {
        protected internal TDbContext DbContextInternal;

        protected internal DbContextOptions Options;

        #region fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _disposed = false;

        #endregion

        #region ctor

        public DbContextBuilder(DbContextOptions<TDbContext> contextOptions)
        {
            Options = contextOptions;
        }

        public DbContextBuilder(TDbContext dbContext)
        {
            DbContextInternal = dbContext;
        }

        #endregion

        #region mothods

        public virtual TDbContext CreateContext()
        {
            if (Options != null)
                return (TDbContext)Activator.CreateInstance(typeof(TDbContext), Options);
            if (DbContextInternal != null)
                return DbContextInternal;
            throw new Exception("Missing DbContext");
        }

        DbContext IDbContextBuilder.CreateContext() => CreateContext();

        #endregion

        #region cleanup

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {

            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DbContextBuilder() => Dispose(false);

        #endregion
    }
}

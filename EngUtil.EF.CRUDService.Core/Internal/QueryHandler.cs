using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EngUtil.EF.CRUDService.Core.Internal
{
    internal class QueryHandler<T> : IDisposable
        where T : DbContext
    {
        private bool _disposeContext;
        private DbContext _dbContext;
        private bool _disposed;


        internal IQueryable<TResult> ToQuery<TSource, TResult>(DbContextBuilder<T> builder, Expression<Func<TSource, TResult>> selector, Expression<Func<TResult, bool>> filter = null, Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null, int skip = 0, int take = 0)
        {
            _disposeContext = builder.DbContextInternal != null && builder == null;
            _dbContext = builder.CreateContext();
            return _dbContext.BuildQuery(selector, filter, orderBy, skip, take);
        }

        #region cleanup

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_disposeContext)
                    _dbContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~QueryHandler() => Dispose(false);

        #endregion
    }
}

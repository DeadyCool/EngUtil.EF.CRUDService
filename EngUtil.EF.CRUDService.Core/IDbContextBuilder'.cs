// --------------------------------------------------------------------------------
// <copyright filename="IDbContextAccessor.cs" date="20-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core
{
    public interface IDbContextBuilder<TContext> : IDbContextBuilder
        where TContext : DbContext
    {
        /// <summary>
        /// A accessor to get the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from the repository
        /// </summary>
        /// <returns></returns>
        new TContext CreateContext();
    }
}


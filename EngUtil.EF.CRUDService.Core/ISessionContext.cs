// --------------------------------------------------------------------------------
// <copyright filename="ISessionContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core
{
    /// <summary>
    /// Represents a accessor to get a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from the repository 
    /// </summary>
    /// <typeparam name="TDbContext">Respresents a type of <see cref="Microsoft.EntityFrameworkCore.DbContext"/> </typeparam>
    public interface ISessionContext<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// A accessor to get the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from the repository
        /// </summary>
        /// <returns></returns>
        TDbContext GetContext();
    }
}

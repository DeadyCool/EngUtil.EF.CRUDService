// --------------------------------------------------------------------------------
// <copyright filename="IDbContextAccessor.cs" date="20-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core.Interfaces
{
    /// <summary>
    /// Represents a accessor to get a <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from the repository 
    /// </summary>
    public interface IDbContextAccessor
    {
        /// <summary>
        /// A accessor to get the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from the repository
        /// </summary>
        /// <returns></returns>
        DbContext CreateContext();
    }
}

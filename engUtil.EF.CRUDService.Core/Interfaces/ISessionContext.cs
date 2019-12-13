// --------------------------------------------------------------------------------
// <copyright filename="ISessionContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core
{
    public interface ISessionContext<TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetContext();
    }
}

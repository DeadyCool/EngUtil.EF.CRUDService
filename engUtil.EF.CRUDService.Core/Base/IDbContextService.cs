// --------------------------------------------------------------------------------
// <copyright filename="IDbContextService.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace engUtil.EF.CRUDService.Core.Base
{
    public interface IDbContextService
    {
        DbContext CreateContext();
    }
}

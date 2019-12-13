// --------------------------------------------------------------------------------
// <copyright filename="IDbContextService.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System.Data.Entity;

namespace engUtil.EF.CRUDService.Base
{
    public interface IDbContextService
    {
        DbContext CreateContext();
    }
}

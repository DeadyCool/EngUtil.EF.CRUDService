// --------------------------------------------------------------------------------
// <copyright filename="DbSetSelector.cs" date="25-06-2020">(c) 2020 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core
{
    public abstract class DbSetSelector
    {
        protected internal DbContext DbContextInternal;

        protected internal DbContextOptions Options;
    }
}

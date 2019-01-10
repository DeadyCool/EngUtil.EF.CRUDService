using Microsoft.EntityFrameworkCore;

namespace engUtil.EF.CRUDService.Core.Base
{
    public interface IDbContextService
    {
        DbContext CreateContext();
    }
}

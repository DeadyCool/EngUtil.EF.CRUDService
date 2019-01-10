using System.Data.Entity;

namespace engUtil.EF.CRUDService.Base
{
    public interface IDbContextService
    {
        DbContext CreateContext();
    }
}

using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.EF.CRUDService.Core;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.CRUDService.CoreASP_Test.Repos
{
    public class PhoneNumberRepository : Repository<PhoneBookContext, PhoneNumberEntity, PhoneNumberModel>
    {
        public PhoneNumberRepository(DbContextOptions<PhoneBookContext> contextOptions) 
            : base(contextOptions)
        {
        }
    }
}

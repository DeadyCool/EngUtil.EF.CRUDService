using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.EF.CRUDService.Core;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.CRUDService.CoreASP_Test.Repos
{
    public class EmailRepository : Repository<PhoneBookContext, EmailEntity, EmailModel>
    {
        public EmailRepository(DbContextOptions<PhoneBookContext> contextOptions) 
            : base(contextOptions)
        {
        }
    }
}

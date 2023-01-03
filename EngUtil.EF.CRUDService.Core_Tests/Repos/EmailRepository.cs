using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class EmailRepository : Repository<AddressBookContext, EmailEntity, EmailModel>
    {
        public EmailRepository(DbContextOptions<AddressBookContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<EmailModel, EmailEntity>> AsEntityExpression => Dto.ToEmailEntity;

        public override Expression<Func<EmailEntity, EmailModel>> AsModelExpression => Dto.ToEmailModel;
    }
}

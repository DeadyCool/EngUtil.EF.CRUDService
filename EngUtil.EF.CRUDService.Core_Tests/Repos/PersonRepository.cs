using engUtil.Dto;
using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Models;


namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class PersonRepository : RepositoryBaseDto<PhoneBookContext, PersonEntity, PersonModel>
    {
        public PersonRepository(ISessionContext<PhoneBookContext> contextService, IMapper dtoMapper) : base(contextService, dtoMapper)
        {
        }
    }
}

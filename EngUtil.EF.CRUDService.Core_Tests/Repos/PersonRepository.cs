using engUtil.Dto;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.Extension;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core_Tests.Repos
{
    public class PersonRepository : RepositoryDto<AddressBookContext, PersonEntity, PersonModel>
    {
        public PersonRepository(DbContextOptions<AddressBookContext> contextOptions, IMapper dtoMapper) 
            : base(contextOptions, dtoMapper)
        {
        }
    }
}

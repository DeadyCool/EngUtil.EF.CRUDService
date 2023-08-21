using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities;

namespace EngUtil.EF.CRUDService.Core_Tests.Models
{
    public class UserModel : ModelBase
    {
        public string Name { get; set; }

        public string Surename { get; set; }

        public string EMail { get; set; }

        public string StreetAddress { get; set; }

        public string Location { get; set; }

        public string State { get; set; }

        public string ZIPCode { get; set; }

        public string Password { get; set; }

        public DateTime DayOfBirth { get; set; }   
    }
}

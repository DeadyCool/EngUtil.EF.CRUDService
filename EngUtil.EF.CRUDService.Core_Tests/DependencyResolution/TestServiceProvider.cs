using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using EngUtil.EF.CRUDService.Core_Tests.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace EngUtil.EF.CRUDService.Core_Tests.DependencyResolution
{
    public class TestServiceProvider
    {
        public static IServiceProvider BuildProvider()
        {
            var builder = new ServiceCollection();

            builder.AddDbContext<AddressBookContext>(options =>
            {     
                options.UseSqlite($"Data Source={TestSettings.DbLitePath}");             
            }, ServiceLifetime.Transient);

            builder.AddTransient<IRepository<PersonModel>, PersonRepository>();
            builder.AddTransient<IRepository<EmailModel>, EmailRepository>();
            builder.AddTransient<IRepository<TelefonnummerModel>, PhoneNumberRepository>();


            return builder.BuildServiceProvider();
        }
    }
}

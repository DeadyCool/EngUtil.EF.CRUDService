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

            builder.AddDbContext<NewspaperContext>(options =>
            {     
                options.UseSqlite($"Data Source={TestSettings.DbLitePath}");             
            }, ServiceLifetime.Transient);

            builder.AddScoped<IRepository<UserModel>, UserRepository>();
            builder.AddScoped<IRepository<NewsModel>, NewsRepository>();
            builder.AddScoped<IRepository<CommentModel>, CommentRepository>();


            return builder.BuildServiceProvider();
        }
    }
}

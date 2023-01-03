using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.CRUDService.CoreASP_Test.Repos;
using EngUtil.EF.CRUDService.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace EngUtil.CRUDService.CoreASP_Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<PhoneBookContext>(
              options =>
              {
                  var connectionString = $"Data Source={Path.Combine(Path.GetTempPath(), "phonebook.sqlite")}";
                  options.UseSqlite(connectionString);
              });            
     
            services.AddScoped<IRepository<PersonModel>, PersonRepository>(); 
            services.AddScoped<IRepository<PhoneNumberModel>, PhoneNumberRepository>();
            services.AddScoped<IRepository<EmailModel>, EmailRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "CRUDService Api Test",
                    Description = "Phonebook API, to Test CRUD-Functions", 
                    Version = "v1"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phonebook Test-Project"));
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PhoneBookContext>())
                {
                    context.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PhoneBookContext>())
                {
                    context.Database.EnsureCreated();              
                }
            }
        }
    }
}

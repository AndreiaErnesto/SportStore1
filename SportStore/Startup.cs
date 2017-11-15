using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SportStore.Models;

namespace SportStore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            //ADIÇÃO DOS SERVIÇOS PRETENDIDOS
            services.AddTransient<IProductRepository, EFProductRepository>();

            //ADICIONAR SERVIÇO QUE DEVOLVA O APPLICATIONDBCONTEXT
            //adicionar o "using Microsoft.EntityFrameworkCore;" lá em cima

            //Ficheiro .json Database=SportsStore - criação de base de dados com este nome
            //Ficheiro .json MultipleActiveResultSets - vários utilizadores a aceder com diferentes querys
            services.AddDbContext<ApplicationDbContext>(options => 
                 options.UseSqlServer(Configuration.GetConnectionString("ConectionStringsSportsStore"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Products}/{action=List}/{id?}");
            });
            //ADICIONAR SEEDDATA
            SeedData.EnsurePopulated(app.ApplicationServices);
        }
    }
}

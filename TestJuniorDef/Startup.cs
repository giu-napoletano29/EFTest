using apitest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using TestJuniorDef.Repositories;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;
using TestJuniorDef.Services;

namespace TestJuniorDef
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging(Env.IsDevelopment()));

            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IBrandRepo, BrandRepo>();
            services.AddScoped<IInfoRequestRepo, InfoRequestRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IInfoRequestService, InfoRequestService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.Use((c, n) =>
            {
                return n();
            });

            app.UseCors(
                options => options.WithOrigins("https://localhost:5002").AllowAnyMethod()
            );


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

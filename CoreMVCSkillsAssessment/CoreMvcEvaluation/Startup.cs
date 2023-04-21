﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Hosting;
using DataAccess.EFCore.Repositories;
using Domain.Interfaces;

namespace CoreMvcEvaluation
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
            services.AddControllersWithViews();

            
            var conn = ConfigurationExtensions.GetConnectionString(this.Configuration, "connString");

            services.AddDbContext<DataAccess.EFCore.AppContext>(options =>
                    options.UseSqlServer(
                    conn,
                    b => b.MigrationsAssembly(typeof(DataAccess.EFCore.AppContext).Assembly.FullName)));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);

            Models.TestContext testContext = new Models.TestContext(new DbContextOptionsBuilder<Models.TestContext>().UseSqlServer(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName={0}\MVCCoreEval.mdf;Integrated Security=True;Trusted_Connection=True;", AppDomain.CurrentDomain.GetData("ContentRootPath") + @"\App_Data")).Options);
            testContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

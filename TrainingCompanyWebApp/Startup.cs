using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainingCompanyWebApp.Models;
using TrainingCompanyWebApp.Models.Interfaces;
using TrainingCompanyWebApp.Models.UnitOfWork;

namespace TrainingCompanyWebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddCloudscribePagination();
            services.AddDbContextPool<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbCon"));
            });
            //services.AddControllersWithViews();
            services.AddMvc().AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
              
                app.UseDeveloperExceptionPage(); //صفحة الخطأ الخاص بالمطور
            }
            else
            {
                app.UseExceptionHandler("Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                 "defaultRoute",
                 "{controller=Home}/{action=Index}/{id?}"
                 );
            });
        }
    }
}

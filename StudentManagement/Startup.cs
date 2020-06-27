using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Models;

namespace StudentManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContextPool<AppDbContext>(
                options=>options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))
                );
            services.AddSingleton<IWelcomService, WelcomeService>();//从头到尾只生成一个
            //services.AddSingleton<IStudentRepository,MockStudentRepository>();
            services.AddScoped<IStudentRepository,SQLStudentRepository>();
                    
                    
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IWelcomService welcomService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//针对开发人员。
            }
            else
            {
                app.UseExceptionHandler();//针对非开发人员。
            }

            app.UseStaticFiles();
            app.UseMvc();
            //第一种路由方式：Conventional Route
            app.UseMvc(builder =>
            {
                 // /Home/Index ——>HomeController Index()
                 builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            /*app.UseMvc(builder =>
            {

            });*/

                //app.UseMvcWithDefaultRoute();//默认路由

        }
    }
}

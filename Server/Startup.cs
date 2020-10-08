using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Data;
using Hangfire.MemoryStorage;
using Server.BusinessContext;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                  options.UseSqlite("Data Source=dbMain.db"));

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });

            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<ISensorsProcessor, SensorsProcessor>();
            services.AddScoped<ITrackingService, ArduinoTrackingService>();
            services.AddScoped<IBashService, BashService>();

            services.AddDirectoryBrowser();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ApplicationContext context, IHostingEnvironment env, IBashService bashService)
        {
            if(env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseCors("AllowAll");
           
            app.UseStaticFiles();
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "photos")),
                RequestPath = "/photos"
            });
            app.UseFileServer();

            app.UseMvc();
            //Starting main tracking service with Hangfire
            RecurringJob.AddOrUpdate((ITrackingService t) => t.WriteMeasuresToDb(), Cron.Hourly);
        }
    }
}

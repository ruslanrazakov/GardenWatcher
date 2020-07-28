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
            services.AddScoped<ITemperatureSensor, TemperatureSensor>();
            services.AddScoped<ILightSensor, LightSensor>();
            services.AddScoped<ArduinoTrackingService>();


            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                     .AllowAnyMethod()
                                                                      .AllowAnyHeader()));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ApplicationContext restaurantContext)
        {
            restaurantContext.Database.EnsureDeleted();
            restaurantContext.Database.EnsureCreated();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseCors("AllowAll");

            app.UseMvc();

        }
    }
}

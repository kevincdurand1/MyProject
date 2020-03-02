using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyProject.Api.Tasks;
using MyProject.Api.Tasks.Jobs;
using MyProject.Infrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MyProject.Api
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
            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            // Add our job
            services.AddSingleton<HelloWorldJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HelloWorldJob),
                cronExpression: "0/5 * * * * ?")); // run every 5 seconds



            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "MyProject API",
                    Version = "v1",
                });
                options.DocInclusionPredicate((docName, description) => true);
            });
            services.AddDbContext<MyProjectContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            //Enable middleware to serve swagger - ui assets(HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProject API V1");
            }); //URL: /swagger 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

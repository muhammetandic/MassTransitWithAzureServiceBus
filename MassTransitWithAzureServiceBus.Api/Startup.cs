using MassTransit;
using MassTransitWithAzureServiceBus.Api.ApplicationDbContext;
using MassTransitWithAzureServiceBus.Api.Consumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace MassTransitWithAzureServiceBus.Api
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

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MassTransitWithAzureServiceBus.Api", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(opt=>opt.EnableSensitiveDataLogging().UseSqlite("Data source=d:\\SampleDB.db"));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderReturnedConsumer>();
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<Startup>()
                    .Build();
                    var connectionString = config["AzureServiceBus"];
                    cfg.Host(connectionString);
                    cfg.ConfigureEndpoints(context);
                });
            });
            
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MassTransitWithAzureServiceBus.Api v1"));
            }

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

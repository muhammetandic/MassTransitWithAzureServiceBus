using MassTransit;
using MassTransitWithAzureServiceBus.OrdersWorker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MassTransitWithAzureServiceBus.Orders
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddMassTransit(x => 
                    {
                        x.AddConsumer<OrderConsumer>();
                        x.UsingAzureServiceBus((context, cfg) =>
                        {
                            var config = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json")
                                .AddUserSecrets<Program>()
                                .Build();
                            var connectionString = config["AzureServiceBus"];
                            cfg.Host(connectionString);
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddMassTransitHostedService(true);
                    //services.AddHostedService<Worker>();
                });
        }
    }
}
